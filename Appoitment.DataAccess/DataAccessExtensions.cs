using Appoitment.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appoitment.DataAccess
{
    public static class DataAccessExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAppoitmentRepository, AppoitmentRepository>();
            
            services.AddDbContext<AppointmentContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection")));
        }
    }
}