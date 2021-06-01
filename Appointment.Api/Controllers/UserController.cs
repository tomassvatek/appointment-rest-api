using System.Threading.Tasks;
using Appointment.DataAccess.Repositories;
using AppointmentWebApp.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentWebApp.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userRepository.GetUsers();
            var mappedUsers = UserMapper.Map(users);
            return Ok(mappedUsers);
        }
    }
}