using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Authentication.Common;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Bookings.Domain.UserAggregate;
using Bookings.Application.Common.Interfaces.Authentication;
using Bookings.Domain.UserAggregate.Enums;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<string>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LoginQueryHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository,
            IClientRepository clientRepository,
            IEmployeeRepository employeeRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<string>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var email = Email.Create(query.Email);
            if (await _userRepository.GetByEmailAsync(email) is not User user)
                return Errors.Authentication.InvalidCredentials;

            //if (!user.IsEmailConfirmed)
            //    return Errors.Authentication.EmailNotConfirmed;

            if (!BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash))
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
