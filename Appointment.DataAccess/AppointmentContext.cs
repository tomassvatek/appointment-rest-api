using Microsoft.EntityFrameworkCore;

namespace Appointment.DataAccess
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext(DbContextOptions<AppointmentContext> options) : base(options)
        {
        }

        public DbSet<Entities.Appointment> Appointments { get; set; }
        public DbSet<Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Appointment>().HasKey(s => s.Id);
            modelBuilder.Entity<Entities.Appointment>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Entities.Appointment>().Property(s => s.Guests).IsRequired();
            modelBuilder.Entity<Entities.Appointment>().Property(s => s.EntityId).IsRequired();
            modelBuilder.Entity<Entities.Appointment>().HasIndex(s => s.EntityId);
            modelBuilder.Entity<Entities.Appointment>().Property(s => s.EntityId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Entities.Appointment>().Property(s => s.CreatedById).IsRequired();
            modelBuilder.Entity<Entities.Appointment>().Property(s => s.StartDate).IsRequired();
            modelBuilder.Entity<Entities.Appointment>().Property(s => s.EndDate).IsRequired();

            // configure one-to-many relation
            modelBuilder.Entity<Entities.Appointment>()
                .HasOne(s => s.CreatedBy)
                .WithMany(s => s.Appointments)
                .HasForeignKey(s => s.CreatedById);

            modelBuilder.Entity<Entities.User>().HasKey(s => s.Id);
            modelBuilder.Entity<Entities.User>().Property(s => s.FirstName).IsRequired();
            modelBuilder.Entity<Entities.User>().Property(s => s.LastName).IsRequired();
            modelBuilder.Entity<Entities.User>().Property(s => s.Email).IsRequired();
            modelBuilder.Entity<Entities.User>().Property(s => s.EntityId).IsRequired();
            modelBuilder.Entity<Entities.User>().HasIndex(s => s.EntityId);
            modelBuilder.Entity<Entities.User>().Property(s => s.EntityId).HasDefaultValueSql("NEWID()");
        }
    }
}