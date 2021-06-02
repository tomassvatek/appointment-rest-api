using System;
using System.Threading.Tasks;
using AppointmentWebApp.Exceptions;
using AppointmentWebApp.Models;
using AppointmentWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentWebApp.Controllers
{
    [Route("users/{userId}/appointments")]
    public class UserAppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public UserAppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments(Guid userId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsAsync(userId);
                return Ok(appointments);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> Get(Guid appointmentId)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentAsync(appointmentId);
                return Ok(appointment);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound($"Appointment '{appointmentId}' not found.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid userId, [FromBody] CreateAppointment appointment)
        {
            try
            {
                var entityId = await _appointmentService.CreateAppointmentAsync(appointment);
                return CreatedAtAction(nameof(Get), new {userId = userId, appointmentId = entityId}, null);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
            catch (InvalidDateRangeException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{appointmentId}")]
        public async Task<IActionResult> Update(Guid userId, Guid appointmentId,
            [FromBody] UpdateAppointment appointment)
        {
            try
            {
                await _appointmentService.UpdateAppointmentAsync(userId, appointmentId, appointment);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ForbiddenAccessException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
            catch (InvalidDateRangeException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{appointmentId}")]
        public async Task<IActionResult> Delete(Guid userId, Guid appointmentId)
        {
            try
            {
                await _appointmentService.DeleteAppointmentAsync(userId, appointmentId);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ForbiddenAccessException e)
            {
                return StatusCode(401, e.Message);
            }
        }
    }
}