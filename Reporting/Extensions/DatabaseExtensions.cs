using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reporting.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddReportingContext(this IServiceCollection services, string connectionString)
        {
            return services.AddEntityFrameworkSqlServer()
                    .AddDbContext<ReportingContext>(contextOptions =>
                    {
                        contextOptions.UseSqlServer(connectionString,
                    serverOptions =>
                    {
                        serverOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    });
                    });
        }
    }
}
