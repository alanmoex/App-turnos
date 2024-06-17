using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<AdminMC> AdminMCs { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<MedicalCenter> MedicalCenters { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<SysAdmin> SysAdmins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medic>()
                .HasMany(m => m.Specialties)
                .WithMany()
                .UsingEntity(j => j.ToTable("MedicSpecialties"));

            base.OnModelCreating(modelBuilder);
        }
    }
}