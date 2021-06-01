using Microsoft.Extensions.DependencyInjection;

namespace AppointmentWebApp.Services
{
    public static class ServicesExtensions
    {
        public static void AddAppointmentsServices(this IServiceCollection services)
        {
            services.AddTransient<IAppointmentService, AppointmentService>();
        }
    }
}