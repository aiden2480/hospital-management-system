using HospitalManagementSystem.Entity;

namespace HospitalManagementSystem.Interfaces;

public interface IMenuService<T> where T : AbstractUser
{
    void MainMenu(T loggedInUser);
}

public interface IDoctorMenuService : IMenuService<Doctor> { }

public interface IPatientMenuService : IMenuService<Patient> { }

public interface IAdminMenuService : IMenuService<Administrator> { }
