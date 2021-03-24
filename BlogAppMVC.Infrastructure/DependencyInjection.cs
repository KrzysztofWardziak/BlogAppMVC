using System;
using System.Collections.Generic;
using System.Text;
using BlogAppMVC.Domain.Interface;
using BlogAppMVC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAppMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            return services;
        }
    }
}
