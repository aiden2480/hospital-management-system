using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Repositories;
using Moq;

namespace HospitalManagementSystem.Test;

internal class DoctorTests : BaseTests
{
    [Test]
    public override void TestGetAll()
    {
        // Arrange
        var doctorRepo = new DoctorRepository(MockDb.Object);

        // Act
        var doctors = doctorRepo.GetAll().ToList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(doctors, Has.Count.EqualTo(3));
            Assert.That(doctors[0], Has.Property("Id").EqualTo(10001).And.Property("FirstName").EqualTo("Patricia"));
            Assert.That(doctors[1], Has.Property("Id").EqualTo(10002).And.Property("FirstName").EqualTo("Timothy"));
            Assert.That(doctors[2], Has.Property("Id").EqualTo(10003).And.Property("FirstName").EqualTo("Jonathan"));
        });
    }

    [Test]
    public override void TestFilterSingle()
    {
        // Arrange
        var doctorRepo = new DoctorRepository(MockDb.Object);

        // Act
        var doctor = doctorRepo.FilterSingle(p => p.Id == 10001);

        // Assert
        Assert.That(doctor, Is.Not.Null);
        Assert.That(doctor.FirstName, Is.EqualTo("Patricia"));
    }

    [Test]
    public override void TestCreate()
    {
        // Arrange
        MockDoctors.Setup(m => m.Add(It.IsAny<Doctor>())).Verifiable();
        MockDb.Setup(m => m.SaveChanges()).Verifiable();

        var doctorRepo = new DoctorRepository(MockDb.Object);
        var doctor = new Doctor
        {
            Id = 20003,
            FirstName = "Ernest",
            LastName = "Martinez",
            Email = "kathycruz@gmail.com",
            PhoneNumber = "0492550332",
            AddrStreetNumber = "26",
            AddrStreet = "Ronald Ferry",
            AddrCity = "New Patricia",
            AddrState = "WA",
            PasswordHash = "password"
        };

        // Act
        doctorRepo.Add(doctor).SaveChanges();

        // Assert
        MockDoctors.Verify(m => m.Add(doctor), Times.Once());
        MockDb.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public override void TestRemove()
    {
        // Arrange
        var doctorRepo = new DoctorRepository(MockDb.Object);
        var doctorToRemove = doctorRepo.FilterSingle(p => p.Id == 10001)!;

        MockDoctors.Setup(m => m.Add(It.IsAny<Doctor>())).Verifiable();
        MockDb.Setup(m => m.SaveChanges()).Verifiable();
        Assume.That(doctorToRemove, Is.Not.Null);

        // Act
        doctorRepo.Remove(doctorToRemove).SaveChanges();

        // Assert
        MockDoctors.Verify(m => m.Remove(doctorToRemove), Times.Once());
        MockDb.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public override void TestGetTotalCount()
    {
        // Arrange
        var doctorRepo = new DoctorRepository(MockDb.Object);
        
        // Act
        var totalCount = doctorRepo.GetTotalCount();

        // Assert
        Assert.That(totalCount, Is.EqualTo(SeedDoctors.Count));
    }
}
