using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HospitalManagementSystem.Test;

internal abstract class BaseTests
{
    protected Mock<HospitalDbContext> MockDb = null!;

    protected Mock<DbSet<Patient>> MockPatients = null!;

    protected Mock<DbSet<Doctor>> MockDoctors = null!;

    protected Mock<DbSet<Appointment>> MockAppointments = null!;

    protected Mock<DbSet<Administrator>> MockAdministrators = null!;

    protected virtual IList<Patient> SeedPatients =>
    [
        new() { Id = 20001, FirstName = "Diane", LastName = "Perkins", Email = "gregg.trant@yahoo.com", PhoneNumber = "0418923028", AddrStreetNumber = "256", AddrStreet = "Bryan Avenue", AddrCity = "Minneapolis", AddrState = "TAS", PasswordHash = "password" },
        new() { Id = 20002, FirstName = "Jacob", LastName = "Villanueva", Email = "nashbrooke@perez-blevins.com", PhoneNumber = "0459609536", AddrStreetNumber = "87", AddrStreet = "Brendan Creek", AddrCity = "Nathanburgh", AddrState = "NSW", PasswordHash = "password" },
        new() { Id = 20003, FirstName = "Ernest", LastName = "Martinez", Email = "kathycruz@gmail.com", PhoneNumber = "0492550332", AddrStreetNumber = "26", AddrStreet = "Ronald Ferry", AddrCity = "New Patricia", AddrState = "WA", PasswordHash = "password" },
    ];

    protected virtual IList<Doctor> SeedDoctors =>
    [
        new Doctor { Id = 10001, FirstName = "Patricia", LastName = "Blake", Email = "patblake@gmail.com", PhoneNumber = "0434567890", AddrStreetNumber = "8", AddrStreet = "Waterloo Street", AddrCity = "Sydney", AddrState = "NSW", PasswordHash = "password" },
        new Doctor { Id = 10002, FirstName = "Timothy", LastName = "Menhennitt", Email = "timmen@yahoo.com", PhoneNumber = "0445384044", AddrStreetNumber = "44", AddrStreet = "Normans Road", AddrCity = "Ullswater", AddrState = "VIC", PasswordHash = "password" },
        new Doctor { Id = 10003, FirstName = "Jonathan", LastName = "Bury", Email = "jbury@hotmail.com", PhoneNumber = "0431938727", AddrStreetNumber = "61", AddrStreet = "Hereford Avenue", AddrCity = "Veitch", AddrState = "SA", PasswordHash = "password" },
    ];

    protected virtual IList<Appointment> SeedAppointments =>
    [
        new Appointment { Id = 40001, DoctorId = 10001, PatientId = 20001, ScheduledTime = new DateTime(2022, 2, 2), Description = "Regular checkup" },
        new Appointment { Id = 40002, DoctorId = 10002, PatientId = 20002, ScheduledTime = new DateTime(2021, 11, 14), Description = "Suspected COVID19" },
        new Appointment { Id = 40003, DoctorId = 10003, PatientId = 20003, ScheduledTime = new DateTime(2019, 7, 7), Description = "My arm is itchy" },
    ];

    protected virtual IList<Administrator> SeedAdministrators =>
    [
        new Administrator { Id = 30001, FirstName = "Nathan", LastName = "Mitchell", PasswordHash = "password" },
        new Administrator { Id = 30002, FirstName = "Frances", LastName = "Brooks", PasswordHash = "password" },
        new Administrator { Id = 30003, FirstName = "Barbara", LastName = "May", PasswordHash = "password" },
    ];

    [SetUp]
    public void Setup()
    {
        MockDb = new Mock<HospitalDbContext>(Mock.Of<IPasswordService>());
        MockPatients = CreateMockDbSet(SeedPatients);
        MockDoctors = CreateMockDbSet(SeedDoctors);
        MockAppointments = CreateMockDbSet(SeedAppointments);
        MockAdministrators = CreateMockDbSet(SeedAdministrators);

        MockDb.Setup(m => m.Set<Patient>()).Returns(MockPatients.Object);
        MockDb.Setup(m => m.Set<Doctor>()).Returns(MockDoctors.Object);
        MockDb.Setup(m => m.Set<Appointment>()).Returns(MockAppointments.Object);
        MockDb.Setup(m => m.Set<Administrator>()).Returns(MockAdministrators.Object);
    }

    [Test]
    public abstract void TestGetAll();

    [Test]
    public abstract void TestFilterSingle();

    [Test]
    public abstract void TestCreate();

    [Test]
    public abstract void TestRemove();

    [Test]
    public abstract void TestGetTotalCount();

    protected static Mock<DbSet<TEntity>> CreateMockDbSet<TEntity>(IList<TEntity> data) where TEntity : class
    {
        var queryable = data.AsQueryable();
        var mockSet = new Mock<DbSet<TEntity>>();
        
        mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        
        return mockSet;
    }
}
