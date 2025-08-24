using Bookings.Application.Common.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Bookings.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
