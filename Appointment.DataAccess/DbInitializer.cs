using System;
using System.Linq;
using Appointment.Entities;

namespace Appointment.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(AppointmentContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
                return;

            var users = new[]
            {
                new User
                {
                    FirstName = "John", LastName = "Doe", CompanyName = "Company A", Email = "john.doe@gmail.com",
                    EntityId = Guid.NewGuid()
                },
                new User
                {
                    FirstName = "Jack", LastName = "Goodman", CompanyName = "Company B",
                    Email = "jack.goodman@gmail.com",
                    EntityId = Guid.NewGuid()
                }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}