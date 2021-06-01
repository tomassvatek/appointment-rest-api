using System.Threading.Tasks;
using AppointmentWebApp.Mappers;
using Appointment.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentWebApp.Controllers
{
    [Route("appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(
            IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var appointments = await _appointmentRepository.GetAppointmentsAsync();
            var mappedAppointments = AppointmentMapper.Map(appointments);

            return Ok(mappedAppointments);
        }
    }
}