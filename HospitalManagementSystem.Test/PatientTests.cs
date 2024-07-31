using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Repositories;
using Moq;

namespace HospitalManagementSystem.Test;

internal class PatientTests : BaseTests
{
    [Test]
    public override void TestGetAll()
    {
        // Arrange
        var patientRepo = new PatientRepository(MockDb.Object);

        // Act
        var patients = patientRepo.GetAll().ToList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(patients, Has.Count.EqualTo(3));
            Assert.That(patients[0], Has.Property("Id").EqualTo(20001).And.Property("FirstName").EqualTo("Diane"));
            Assert.That(patients[1], Has.Property("Id").EqualTo(20002).And.Property("FirstName").EqualTo("Jacob"));
            Assert.That(patients[2], Has.Property("Id").EqualTo(20003).And.Property("FirstName").EqualTo("Ernest"));
        });
    }

    [Test]
    public override void TestFilterSingle()
    {
        // Arrange
        var patientRepo = new PatientRepository(MockDb.Object);

        // Act
        var patient = patientRepo.FilterSingle(p => p.Id == 20001);

        // Assert
        Assert.That(patient, Is.Not.Null);
        Assert.That(patient.FirstName, Is.EqualTo("Diane"));
    }

    [Test]
    public override void TestCreate()
    {
        // Arrange
        MockPatients.Setup(m => m.Add(It.IsAny<Patient>())).Verifiable();
        MockDb.Setup(m => m.SaveChanges()).Verifiable();

        var patientRepo = new PatientRepository(MockDb.Object);
        var patient = new Patient
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
        patientRepo.Add(patient).SaveChanges();

        // Assert
        MockPatients.Verify(m => m.Add(patient), Times.Once());
        MockDb.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public override void TestRemove()
    {
        // Arrange
        var patientRepo = new PatientRepository(MockDb.Object);
        var patientToRemove = patientRepo.FilterSingle(p => p.Id == 20001)!;

        MockPatients.Setup(m => m.Add(It.IsAny<Patient>())).Verifiable();
        MockDb.Setup(m => m.SaveChanges()).Verifiable();
        Assume.That(patientToRemove, Is.Not.Null);

        // Act
        patientRepo.Remove(patientToRemove).SaveChanges();

        // Assert
        MockPatients.Verify(m => m.Remove(patientToRemove), Times.Once());
        MockDb.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public override void TestGetTotalCount()
    {
        // Arrange
        var patientRepo = new PatientRepository(MockDb.Object);
        
        // Act
        var totalCount = patientRepo.GetTotalCount();

        // Assert
        Assert.That(totalCount, Is.EqualTo(SeedPatients.Count));
    }
}
