using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Repositories;
using Moq;

namespace HospitalManagementSystem.Test;

internal class AdminTests : BaseTests
{
    [Test]
    public override void TestGetAll()
    {
        // Arrange
        var adminRepo = new AdministratorRepository(MockDb.Object);

        // Act
        var admins = adminRepo.GetAll().ToList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(admins, Has.Count.EqualTo(3));
            Assert.That(admins[0], Has.Property("Id").EqualTo(30001).And.Property("FirstName").EqualTo("Nathan"));
            Assert.That(admins[1], Has.Property("Id").EqualTo(30002).And.Property("FirstName").EqualTo("Frances"));
            Assert.That(admins[2], Has.Property("Id").EqualTo(30003).And.Property("FirstName").EqualTo("Barbara"));
        });
    }

    [Test]
    public override void TestFilterSingle()
    {
        // Arrange
        var adminRepo = new AdministratorRepository(MockDb.Object);

        // Act
        var admin = adminRepo.FilterSingle(p => p.Id == 30001);

        // Assert
        Assert.That(admin, Is.Not.Null);
        Assert.That(admin.FirstName, Is.EqualTo("Nathan"));
    }

    [Test]
    public override void TestCreate()
    {
        // Arrange
        MockAdministrators.Setup(m => m.Add(It.IsAny<Administrator>())).Verifiable();
        MockDb.Setup(m => m.SaveChanges()).Verifiable();

        var adminRepo = new AdministratorRepository(MockDb.Object);
        var admin = new Administrator
        {
            Id = 30003,
            FirstName = "Ernest",
            LastName = "Martinez",
            PasswordHash = "password"
        };

        // Act
        adminRepo.Add(admin).SaveChanges();

        // Assert
        MockAdministrators.Verify(m => m.Add(admin), Times.Once());
        MockDb.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public override void TestRemove()
    {
        // Arrange
        var adminRepo = new AdministratorRepository(MockDb.Object);
        var adminToRemove = adminRepo.FilterSingle(p => p.Id == 30001)!;

        MockAdministrators.Setup(m => m.Add(It.IsAny<Administrator>())).Verifiable();
        MockDb.Setup(m => m.SaveChanges()).Verifiable();
        Assume.That(adminToRemove, Is.Not.Null);

        // Act
        adminRepo.Remove(adminToRemove).SaveChanges();

        // Assert
        MockAdministrators.Verify(m => m.Remove(adminToRemove), Times.Once);
        MockDb.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Test]
    public override void TestGetTotalCount()
    {
        // Arrange
        var adminRepo = new AdministratorRepository(MockDb.Object);
        
        // Act
        var totalCount = adminRepo.GetTotalCount();

        // Assert
        Assert.That(totalCount, Is.EqualTo(SeedAdministrators.Count));
    }
}
