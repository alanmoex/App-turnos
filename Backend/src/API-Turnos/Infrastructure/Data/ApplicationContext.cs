using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Newtonsoft.Json.Linq;
using Domain.Enums;

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
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);

            modelBuilder.Entity<Patient>().HasData(CreatePatientDataSeed());
            modelBuilder.Entity<Medic>().HasData(CreateMedicDataSeed());
            modelBuilder.Entity<Specialty>().HasData(CreateSpecialtyDataSeed());
            modelBuilder.Entity<MedicalCenter>().HasData(CreateMedicalCenterDataSeed());
            modelBuilder.Entity<WorkSchedule>().HasData(CreateWorkScheduleDataSeed());
            modelBuilder.Entity<SysAdmin>().HasData(CreateSysAdminDataSeed());
            modelBuilder.Entity<AdminMC>().HasData(CreateAdminMCDataSeed());
            modelBuilder.Entity<Appointment>().HasData(CreateAppointmentDataSeed());

            //Sql server impone restricciones sobre multiples caminos de eliminacion en cascada, por lo
            //tanto se necesita configurar manualmente el comportamiento de eliminacion.
            /*modelBuilder.Entity<Appointment>()
               .HasOne(a => a.Medic)
               .WithMany(m => m.Appointments)
               .HasForeignKey("MedicId")
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalCenter>()
               .HasMany(m => m.AdminMCs)
               .WithOne(a => a.MedicalCenter)
               .HasForeignKey("MedicalCenterId")
               .OnDelete(DeleteBehavior.Cascade);*/

            //Disable all default relationship cascade delete behavior
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            // Relación muchos a muchos entre Medic y Specialty
            modelBuilder.Entity<Medic>()
                .HasMany(m => m.Specialties)
                .WithMany()
                .UsingEntity(j => j
                    .ToTable("MedicSpecialties")
                    .HasData(CreateMedicSpecialtyDataSeed())
                    );

            // Relación muchos a muchos entre Medic y WorkSchedule
            modelBuilder.Entity<Medic>()
                .HasMany(m => m.WorkSchedules)
                .WithMany()
                .UsingEntity(j => j
                    .ToTable("MedicWorkSchedules")
                    .HasData(CreateMedicWorkScheduleDataSeed())
                    );

        }

        private Patient[] CreatePatientDataSeed()
        {
            Patient[] result = new Patient[]
            {
                new Patient { Id = 1, Name = "John", LastName = "Doe", Email = "john.doe@example.com", Password = "123" },
                new Patient { Id = 2, Name = "Emily", LastName = "Johnson", Email = "emily.johnson@example.com", Password = "123" },
                new Patient { Id = 3, Name = "George", LastName = "Peterson", Email = "george.peterson@example.com", Password = "123" }
            };
            return result;
        }

        private object[] CreateMedicDataSeed()
        {
            object[] result = new[]
            {
                new { Id = 1, Name = "Michael", LastName = "Brown", LicenseNumber = "123456", MedicalCenterId = 1, },
                new { Id = 2, Name = "Jane", LastName = "Smith", LicenseNumber = "654321", MedicalCenterId = 1 },
                new { Id = 3, Name = "Peter", LastName = "Jackson", LicenseNumber = "321123", MedicalCenterId = 2 }
            };
            return result;
        }

        private Specialty[] CreateSpecialtyDataSeed()
        {
            Specialty[] result = new Specialty[]
            {
                new Specialty { Id = 1, Name = "Cardiology" },
                new Specialty { Id = 2, Name = "Neurology" },
                new Specialty { Id = 3, Name = "Pediatrics" }
            };
            return result;
        }

        private MedicalCenter[] CreateMedicalCenterDataSeed()
        {
            MedicalCenter[] result = new MedicalCenter[]
            {
                new MedicalCenter { Id = 1, Name = "General Hospital" },
                new MedicalCenter { Id = 2, Name = "City Clinic" }
            };
            return result;
        }

        private WorkSchedule[] CreateWorkScheduleDataSeed()
        {
            WorkSchedule[] result = new WorkSchedule[]
            {
                new WorkSchedule { Id = 1, Day = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                new WorkSchedule { Id = 2, Day = DayOfWeek.Tuesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                new WorkSchedule { Id = 3, Day = DayOfWeek.Wednesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                new WorkSchedule { Id = 4, Day = DayOfWeek.Thursday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                new WorkSchedule { Id = 5, Day = DayOfWeek.Friday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) }
            };
            return result;
        }

        private SysAdmin[] CreateSysAdminDataSeed()
        {
            SysAdmin[] result = new SysAdmin[]
            {
                new SysAdmin { Id = 4, Name = "Admin1", Email = "sysadmin1@example.com", Password = "admin123" },
                new SysAdmin { Id = 5, Name = "Admin2", Email = "sysadmin2@example.com", Password = "admin123" }
            };
            return result;
        }

        private object[] CreateAdminMCDataSeed()
        {
            object[] result = new[]
            {
                new { Id = 6, Name = "Admin 1", Email = "admin1@example.com", Password = "password1", MedicalCenterId = 1 },
                new { Id = 7, Name = "Admin 2", Email = "admin2@example.com", Password = "password2", MedicalCenterId = 2 }
            };
            return result;
        }

        private object[] CreateAppointmentDataSeed()
        {
            object[] result = new[]
            {
                new  { Id = 1, AppointmentDateTime = new DateTime(2023, 6, 21, 10, 0, 0), MedicId = 1, PatientId = 1, MedicalCenterId = 1, Status = AppointmentStatus.Taken},
                new  { Id = 2, AppointmentDateTime = new DateTime(2023, 6, 22, 11, 0, 0), MedicId = 2, PatientId = 2, MedicalCenterId = 2, Status = AppointmentStatus.Taken},
                new  { Id = 3, AppointmentDateTime = new DateTime(2023, 6, 23, 12, 0, 0), MedicId = 3, PatientId = 3, MedicalCenterId = 1, Status = AppointmentStatus.Taken}
            };
            return result;
        }

        private object[] CreateMedicSpecialtyDataSeed()
        {
            object[] result = new[]
            {
                new { MedicId = 1, SpecialtiesId = 1 },
                new { MedicId = 2, SpecialtiesId = 1 },
                new { MedicId = 2, SpecialtiesId = 2 },
                new { MedicId = 3, SpecialtiesId = 1 },
                new { MedicId = 3, SpecialtiesId = 2 },
                new { MedicId = 3, SpecialtiesId = 3 }
            };
            return result;
        }

        private object[] CreateMedicWorkScheduleDataSeed()
        {
            object[] result = new[]
            {
                new { MedicId = 1, WorkSchedulesId = 1 },
                new { MedicId = 1, WorkSchedulesId = 2 },
                new { MedicId = 1, WorkSchedulesId = 4 },
                new { MedicId = 2, WorkSchedulesId = 2 },
                new { MedicId = 2, WorkSchedulesId = 5 },
                new { MedicId = 3, WorkSchedulesId = 3 },
                new { MedicId = 3, WorkSchedulesId = 1 },
                new { MedicId = 3, WorkSchedulesId = 5 }
            };
            return result;
        }

        private object[] CreateMedicalCenterSpecialtiesDataSeed()
        {
            object[] result = new[]
            {
                new { MedicalCenterId = 1, SpecialtiesId =1 },
                new { MedicalCenterId = 1, SpecialtiesId =2 },
                new { MedicalCenterId = 2, SpecialtiesId =2 },
                new { MedicalCenterId = 2, SpecialtiesId =3 },
            };
            return result;
        }
    }
}