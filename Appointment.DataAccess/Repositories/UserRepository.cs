using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetUsers();
        Task<User> GetUser(Guid entityId);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppointmentContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<User>> GetUsers()
            => await Context.Users.ToListAsync();

        public async Task<User> GetUser(Guid entityId)
            => await Context.Users.SingleOrDefaultAsync(s => s.EntityId == entityId);
    }
}