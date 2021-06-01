using Appointment.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DataAccess
{
    public static class DataAccessExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            
            services.AddDbContext<AppointmentContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection")));
        }
    }
}