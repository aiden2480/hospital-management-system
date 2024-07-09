using HospitalManagementSystem.Entity;

namespace HospitalManagementSystem.Interfaces;

public interface IMenuService<T> where T : LoginCapableUser
{
    void MainMenu(T loggedInUser);
}

public interface IDoctorMenuService : IMenuService<Doctor> { }

public interface IPatientMenuService : IMenuService<Patient> { }

public interface IAdminMenuService : IMenuService<Administrator> { }
