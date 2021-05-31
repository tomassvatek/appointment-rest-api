using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appoitment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appoitment.DataAccess.Repositories
{
    public interface IAppoitmentRepository
    {
        Task<IReadOnlyList<Appointment>> GetAppointments(int createdBy);
    }

    public class AppoitmentRepository : BaseRepository, IAppoitmentRepository
    {
        public AppoitmentRepository(AppointmentContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Appointment>> GetAppointments(int createdBy)
            => await Context.Appointments
                .Where(s => s.CreatedById == createdBy)
                .ToListAsync();
    }
}