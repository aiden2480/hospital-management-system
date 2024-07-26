using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace HospitalManagementSystem.Entity;

public class HospitalDbContext(IPasswordService passwordService) : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Administrator> Administrators { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(ConfigurationManager.ConnectionStrings["HospitalDbContext"].ConnectionString);

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

        // Insert dummy data
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { Id = 10001, FirstName = "Patricia", LastName = "Blake", Email = "patblake@gmail.com", PhoneNumber = "0434567890", AddrStreetNumber = "8", AddrStreet = "Waterloo Street", AddrCity = "Sydney", AddrState = "NSW", PasswordHash = Hash("password") },
            new Doctor { Id = 10002, FirstName = "Timothy", LastName = "Menhennitt", Email = "timmen@yahoo.com", PhoneNumber = "0445384044", AddrStreetNumber = "44", AddrStreet = "Normans Road", AddrCity = "Ullswater", AddrState = "VIC", PasswordHash = Hash("password") },
            new Doctor { Id = 10003, FirstName = "Jonathan", LastName = "Bury", Email = "jbury@hotmail.com", PhoneNumber = "0431938727", AddrStreetNumber = "61", AddrStreet = "Hereford Avenue", AddrCity = "Veitch", AddrState = "SA", PasswordHash = Hash("password") },
            new Doctor { Id = 10004, FirstName = "Michael", LastName = "Freeman", Email = "howardthomas@gmail.com", PhoneNumber = "0424243806", AddrStreetNumber = "302", AddrStreet = "Regina Greens", AddrCity = "North Mark", AddrState = "NSW", PasswordHash = Hash("password") },
            new Doctor { Id = 10005, FirstName = "Stephanie", LastName = "Walker", Email = "dporter@davis-bowen.biz", PhoneNumber = "0456829556", AddrStreetNumber = "331", AddrStreet = "Martin Turnpike", AddrCity = "Sierraborough", AddrState = "NSW", PasswordHash = Hash("password") },
            new Doctor { Id = 10006, FirstName = "Allison", LastName = "Robinson", Email = "pacetim@hotmail.com", PhoneNumber = "0477108964", AddrStreetNumber = "56", AddrStreet = "Oconnor Flat", AddrCity = "Ponceview", AddrState = "TAS", PasswordHash = Hash("password") },
            new Doctor { Id = 10007, FirstName = "Jacob", LastName = "Brown", Email = "brooke54@martin.com", PhoneNumber = "0467626654", AddrStreetNumber = "231", AddrStreet = "Craig Rapid", AddrCity = "East Hannah", AddrState = "QLD", PasswordHash = Hash("password") },
            new Doctor { Id = 10008, FirstName = "Gary", LastName = "Pierce", Email = "qturner@hotmail.com", PhoneNumber = "0419650199", AddrStreetNumber = "307", AddrStreet = "Monica Inlet", AddrCity = "Valeriechester", AddrState = "ACT", PasswordHash = Hash("password") },
            new Doctor { Id = 10009, FirstName = "Sheila", LastName = "Lee", Email = "heathermorales@mills.com", PhoneNumber = "0474645295", AddrStreetNumber = "9", AddrStreet = "Michael Junction", AddrCity = "South Randy", AddrState = "WA", PasswordHash = Hash("password") },
            new Doctor { Id = 10010, FirstName = "Timothy", LastName = "Anderson", Email = "kellyschwartz@yahoo.com", PhoneNumber = "0499251707", AddrStreetNumber = "309", AddrStreet = "Burke Unions", AddrCity = "Garciafort", AddrState = "VIC", PasswordHash = Hash("password") });

        modelBuilder.Entity<Patient>().HasData(
            new Patient { Id = 20001, FirstName = "Diane", LastName = "Perkins", Email = "gregg.trant@yahoo.com", PhoneNumber = "0418923028", AddrStreetNumber = "256", AddrStreet = "Bryan Avenue", AddrCity = "Minneapolis", AddrState = "TAS", PasswordHash = Hash("password") },
            new Patient { Id = 20002, FirstName = "Jacob", LastName = "Villanueva", Email = "nashbrooke@perez-blevins.com", PhoneNumber = "0459609536", AddrStreetNumber = "87", AddrStreet = "Brendan Creek", AddrCity = "Nathanburgh", AddrState = "NSW", PasswordHash = Hash("password") },
            new Patient { Id = 20003, FirstName = "Ernest", LastName = "Martinez", Email = "kathycruz@gmail.com", PhoneNumber = "0492550332", AddrStreetNumber = "26", AddrStreet = "Ronald Ferry", AddrCity = "New Patricia", AddrState = "WA", PasswordHash = Hash("password") },
            new Patient { Id = 20004, FirstName = "Sheila", LastName = "Bowen", Email = "leeramirez@duffy.net", PhoneNumber = "0437652545", AddrStreetNumber = "240", AddrStreet = "Hill Tunnel", AddrCity = "North Brandonmouth", AddrState = "WA", PasswordHash = Hash("password") },
            new Patient { Id = 20005, FirstName = "Alexander", LastName = "Taylor", Email = "dburnett@hotmail.com", PhoneNumber = "0490232432", AddrStreetNumber = "322", AddrStreet = "James Village", AddrCity = "Reedhaven", AddrState = "NT", PasswordHash = Hash("password") },
            new Patient { Id = 20006, FirstName = "Michael", LastName = "Johnson", Email = "wmyers@ross.biz", PhoneNumber = "0480774922", AddrStreetNumber = "369", AddrStreet = "Calvin Loop", AddrCity = "Bretttown", AddrState = "NSW", PasswordHash = Hash("password") },
            new Patient { Id = 20007, FirstName = "Steven", LastName = "Cohen", Email = "lauren61@castillo.info", PhoneNumber = "0413351145", AddrStreetNumber = "178", AddrStreet = "Mary Valleys", AddrCity = "Kellyport", AddrState = "QLD", PasswordHash = Hash("password") },
            new Patient { Id = 20008, FirstName = "Nathan", LastName = "Martin", Email = "robert67@yahoo.com", PhoneNumber = "0444997472", AddrStreetNumber = "461", AddrStreet = "Fernandez Ridge", AddrCity = "New Jillberg", AddrState = "NSW", PasswordHash = Hash("password") },
            new Patient { Id = 20009, FirstName = "Jonathan", LastName = "Lewis", Email = "ujohnson@hotmail.com", PhoneNumber = "0456237153", AddrStreetNumber = "403", AddrStreet = "Payne Loaf", AddrCity = "Brandonland", AddrState = "SA", PasswordHash = Hash("password") },
            new Patient { Id = 20010, FirstName = "Jimmy", LastName = "Reyes", Email = "grace48@yahoo.com", PhoneNumber = "0488841764", AddrStreetNumber = "269", AddrStreet = "Edward Isle", AddrCity = "West Natashabury", AddrState = "ACT", PasswordHash = Hash("password") });

        modelBuilder.Entity<Administrator>().HasData(
            new Administrator { Id = 30001, FirstName = "Nathan", LastName = "Mitchell", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30002, FirstName = "Frances", LastName = "Brooks", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30003, FirstName = "Barbara", LastName = "May", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30004, FirstName = "Timothy", LastName = "Garcia", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30005, FirstName = "Billy", LastName = "Sullivan", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30006, FirstName = "Michael", LastName = "Winters", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30007, FirstName = "Robin", LastName = "Munoz", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30008, FirstName = "Dana", LastName = "Reese", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30009, FirstName = "Gregory", LastName = "Brown", PasswordHash = Hash("p@ssw0rd") },
            new Administrator { Id = 30010, FirstName = "Jill", LastName = "Mendoza", PasswordHash = Hash("p@ssw0rd") });

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment { Id = 40001, DoctorId = 10001, PatientId = 20001, ScheduledTime = DateTime.Now.AddHours(89), Description = "Regular checkup" },
            new Appointment { Id = 40002, DoctorId = 10003, PatientId = 20008, ScheduledTime = DateTime.Now.AddHours(43), Description = "Suspected COVID19" },
            new Appointment { Id = 40003, DoctorId = 10001, PatientId = 20004, ScheduledTime = DateTime.Now.AddHours(100), Description = "My arm is itchy" },
            new Appointment { Id = 40004, DoctorId = 10007, PatientId = 20008, ScheduledTime = DateTime.Now.AddHours(119), Description = "Long headaches" },
            new Appointment { Id = 40005, DoctorId = 10002, PatientId = 20003, ScheduledTime = DateTime.Now.AddHours(158), Description = "Weird rash on leg that looks like Europe" },
            new Appointment { Id = 40006, DoctorId = 10004, PatientId = 20006, ScheduledTime = DateTime.Now.AddHours(96), Description = "Follow-up for high blood pressure" },
            new Appointment { Id = 40007, DoctorId = 10001, PatientId = 20008, ScheduledTime = DateTime.Now.AddHours(73), Description = "My knee makes a weird clicking sound" },
            new Appointment { Id = 40008, DoctorId = 10004, PatientId = 20008, ScheduledTime = DateTime.Now.AddHours(143), Description = "Post-surgery checkin" },
            new Appointment { Id = 40009, DoctorId = 10004, PatientId = 20010, ScheduledTime = DateTime.Now.AddHours(137), Description = "Cat allergy" },
            new Appointment { Id = 40010, DoctorId = 10002, PatientId = 20003, ScheduledTime = DateTime.Now.AddHours(140), Description = "Annual physical exam" },
            new Appointment { Id = 40011, DoctorId = 10010, PatientId = 20002, ScheduledTime = DateTime.Now.AddHours(49), Description = "Left eye continuously twitching" },
            new Appointment { Id = 40012, DoctorId = 10010, PatientId = 20002, ScheduledTime = DateTime.Now.AddHours(161), Description = "Stomach pain after spicy foods" },
            new Appointment { Id = 40013, DoctorId = 10002, PatientId = 20003, ScheduledTime = DateTime.Now.AddHours(167), Description = "Flu shot" },
            new Appointment { Id = 40014, DoctorId = 10002, PatientId = 20005, ScheduledTime = DateTime.Now.AddHours(101), Description = "Back hurts" },
            new Appointment { Id = 40015, DoctorId = 10007, PatientId = 20005, ScheduledTime = DateTime.Now.AddHours(48), Description = "Constantly tired" },
            new Appointment { Id = 40016, DoctorId = 10002, PatientId = 20009, ScheduledTime = DateTime.Now.AddHours(42), Description = "My kid stuck a bead up their nose" },
            new Appointment { Id = 40017, DoctorId = 10001, PatientId = 20004, ScheduledTime = DateTime.Now.AddHours(161), Description = "Trouble sleeping at night" },
            new Appointment { Id = 40018, DoctorId = 10002, PatientId = 20001, ScheduledTime = DateTime.Now.AddHours(55), Description = "Asthma prescription" },
            new Appointment { Id = 40019, DoctorId = 10001, PatientId = 20008, ScheduledTime = DateTime.Now.AddHours(116), Description = "Hair falling out in clumps" },
            new Appointment { Id = 40020, DoctorId = 10006, PatientId = 20005, ScheduledTime = DateTime.Now.AddHours(78), Description = "I have a strange craving for chalk" });
    }

    private string Hash(string password)
        => passwordService.HashPassword(password);
}
