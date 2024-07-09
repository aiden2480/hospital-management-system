using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Services;

internal class DoctorMenuService(HospitalDbContext db) : IDoctorMenuService
{
    public void MainMenu(Doctor loggedInDoctor)
    {
        Console.WriteLine($"You are logged in as a doctor and your name is {loggedInDoctor.FirstName} {loggedInDoctor.LastName}");
    }
}
