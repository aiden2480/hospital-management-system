using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Repositories;
using Moq;

namespace HospitalManagementSystem.Test;

internal class AppointmentTests : BaseTests
{
    [Test]
    public override void TestGetAll()
    {
        // Arrange
        var apptRepo = new AppointmentRepository(MockDb.Object);

        // Act
        var appts = apptRepo.GetAll().ToList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(appts, Has.Count.EqualTo(3));
            Assert.That(appts[0], Has.Property("Id").EqualTo(40001).And.Property("Description").EqualTo("Regular checkup"));
            Assert.That(appts[1], Has.Property("Id").EqualTo(40002).And.Property("Description").EqualTo("Suspected COVID19"));
            Assert.That(appts[2], Has.Property("Id").EqualTo(40003).And.Property("Description").EqualTo("My arm is itchy"));
        });
    }

    [Test]
    public override void TestFilterSingle()
    {
        // Arrange
        var apptRepo = new AppointmentRepository(MockDb.Object);

        // Act
        var appt = apptRepo.FilterSingle(p => p.Id == 40001);

        // Assert
        Assert.That(appt, Is.Not.Null);
        Assert.That(appt.Description, Is.EqualTo("Regular checkup"));
    }

    [Test]
    public override void TestCreate()
    {
        // Arrange
        MockAppointments.Setup(m => m.Add(It.IsAny<Appointment>())).Verifiable();
        MockDb.Setup(m => m.SaveChanges()).Verifiable();

        var apptRepo = new AppointmentRepository(MockDb.Object);
        var appt = new Appointment
        {
            Id = 40003,
            Description = "Ernest",
            DoctorId = 10001,
            PatientId = 20001,
            ScheduledTime = DateTime.Now,
        };

        // Act
        apptRepo.Add(appt).SaveChanges();

        // Assert
        MockAppointments.Verify(m => m.Add(appt), Times.Once());
        MockDb.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public override void TestRemove()
    {
        // Arrange
        var apptRepo = new AppointmentRepository(MockDb.Object);
        var apptToRemove = apptRepo.FilterSingle(p => p.Id == 40001)!;

        MockAppointments.Setup(m => m.Add(It.IsAny<Appointment>())).Verifiable();
        MockDb.Setup(m => m.SaveChanges()).Verifiable();
        Assume.That(apptToRemove, Is.Not.Null);

        // Act
        apptRepo.Remove(apptToRemove).SaveChanges();

        // Assert
        MockAppointments.Verify(m => m.Remove(apptToRemove), Times.Once);
        MockDb.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public override void TestGetTotalCount()
    {
        // Arrange
        var apptRepo = new AppointmentRepository(MockDb.Object);
        
        // Act
        var totalCount = apptRepo.GetTotalCount();

        // Assert
        Assert.That(totalCount, Is.EqualTo(SeedAppointments.Count));
    }
}
