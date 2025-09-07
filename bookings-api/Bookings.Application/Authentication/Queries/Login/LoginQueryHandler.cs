using Bookings.Application.Authentication.Common;
using Bookings.Application.Common.Interfaces.Authentication;
using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.Enums;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IClientRepository clientRepository,
        IEmployeeRepository employeeRepository,
        IPasswordHasher passwordHasher) : IRequestHandler<LoginQuery, ErrorOr<string>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<ErrorOr<string>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var email = Email.Create(query.Email);
            if (await _userRepository.GetByEmailAsync(email) is not User user)
                return Errors.Authentication.InvalidCredentials;

            //if (!user.IsEmailConfirmed)
            //    return Errors.Authentication.EmailNotConfirmed;

            if (!_passwordHasher.Verify(query.Password, user.PasswordHash))
                return Errors.Authentication.InvalidCredentials;

            Guid roleId = Guid.Empty;

            if (user.Role == UserRole.Client)
            {
                if (await _clientRepository.GetByUserIdAsync(user.Id) is not Client client)
                    throw new Exception($"Could not find client associated with UserId: {user.Id.Value}");

                roleId = client.Id.Value;
            }
            else if (user.Role == UserRole.Employee)
            {
                if (await _employeeRepository.GetByUserIdAsync(user.Id) is not Employee employee)
                    throw new Exception($"Could not find employee associated with UserId: {user.Id.Value}");

                roleId = employee.Id.Value;
            }

            var token = _jwtTokenGenerator.GenerateToken(user, roleId);

            return token;
        }
    }
}
