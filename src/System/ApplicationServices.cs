using Microsoft.Extensions.DependencyInjection;
using ServeUp.System;

namespace ServeUp.System
{
    public static class ApplicationServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityProvider, IdentityProvider>();
            services.AddScoped<ExecutionContext>();
            services.AddTransient<IPasswordHashService, PasswordHashService>();
            services.AddTransient<ITokenService, JwtService>();
            services.AddTransient<AuthValidator>();
            services.AddTransient<AuthorisationMiddleware>();
            services.AddTransient<ExceptionHandlingMiddleware>();
        }
    }
}