using DataAccess.Interface;
using DataAccess.JwtServices;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<ISupportFormRepository, SupportFormRepository>();
            services.AddTransient<ITokenHelper, JwtHelper>();
            return services;
        }
    }
}
