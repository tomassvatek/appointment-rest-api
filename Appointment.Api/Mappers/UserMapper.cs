using System.Collections.Generic;
using System.Linq;
using Appointment.Entities;

namespace AppointmentWebApp.Mappers
{
    public static class UserMapper
    {
        public static IReadOnlyList<Models.User> Map(IReadOnlyList<User> users)
            => users.Select(Map).ToList();
        
        public static Models.User Map(User user)
            => new Models.User
            {
                UserId = user.EntityId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Company = user.CompanyName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
    }
}