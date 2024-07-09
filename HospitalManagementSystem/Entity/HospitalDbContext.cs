using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HospitalManagementSystem.Entity;

public class HospitalDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Administrator> Administrators { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public IEnumerable<LoginCapableUser> LoginCapableUsers
        => Doctors.AsEnumerable().Cast<LoginCapableUser>()
            .Concat(Patients.Cast<LoginCapableUser>())
            .Concat(Administrators.Cast<LoginCapableUser>());

    public HospitalDbContext()
        => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"Server={Environment.MachineName};Database=HospitalManagementSystem;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set the auto increment for these fields to start at certain values
        modelBuilder
            .Entity<Doctor>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(10001, 1);

        modelBuilder
            .Entity<Patient>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(20001, 1);

        modelBuilder
            .Entity<Administrator>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(30001, 1);

        modelBuilder
            .Entity<Appointment>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(40001, 1);

        // We should always have a default admin login
        modelBuilder.Entity<Administrator>().HasData(
            new Administrator { Id = 30001, Password = "p@assw0rd" });

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { Id = 10001, FirstName = "Patricia", LastName = "Blake", Email = "patblake@gmail.com", PhoneNumber = "1234567890", AddrStreetNumber = "8", AddrStreet = "Waterloo Street", AddrCity = "Sydney", AddrState = "NSW", Password = "password" });

        modelBuilder.Entity<Patient>().HasData(
            new Patient { Id = 20001, FirstName = "Diane", LastName = "Perkins", Email = "gregg.trant@yahoo.com", PhoneNumber = "0418923028", AddrStreetNumber = "256", AddrStreet = "Bryan Avenue", AddrCity = "Minneapolis", AddrState = "Minnesota", Password = "password" });

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment { Id = 40001, DoctorId = 10001, PatientId = 20001, ScheduledTime = DateTime.Now, Description = "Regular checkup" });
    }
}
