using System;
using System.Threading.Tasks;
using Appoitment.DataAccess.Repositories;
using AppoitmentWebApp.Mappers;
using AppoitmentWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppoitmentWebApp.Controllers
{
    [Route("appointments")]
    public class AppoitmentController : ControllerBase
    {
        private readonly IAppoitmentRepository _appoitmentRepository;

        public AppoitmentController(IAppoitmentRepository appoitmentRepository)
        {
            _appoitmentRepository = appoitmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _appoitmentRepository.GetAppointments();
            var mappedResult = AppoitmentMapper.Map(result);
            
            return Ok(mappedResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EditAppoitment appoitment)
        {
            var entity = AppoitmentMapper.Map(appoitment);
            await _appoitmentRepository.CreateAppointment(entity);

            return NoContent();
        }

        [HttpPut("{appointmentId}")]
        public Task<IActionResult> Update(int appointmentId, EditAppoitment appoitment)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{appointmentId}")]
        public async Task<IActionResult> Delete(int appointmentId)
        {
            await _appoitmentRepository.DeleteAppoitment(appointmentId);
            return NoContent();
        }
    }
}