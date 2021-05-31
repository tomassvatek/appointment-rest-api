using System;
using System.Threading.Tasks;
using Appoitment.DataAccess.Repositories;
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
            var result = await _appoitmentRepository.GetAppointments(1);
            return Ok(result);
        }

        [HttpPost]
        public Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{appointmentId}")]
        public Task<IActionResult> Update(int appointmentId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{appointmentId}")]
        public Task<IActionResult> Delete(int appointmentId)
        {
            throw new NotImplementedException();
        }
    }
}