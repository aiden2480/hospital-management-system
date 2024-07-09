using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Services;

internal class PatientMenuService(HospitalDbContext db) : IPatientMenuService
{
    public void MainMenu(Patient loggedInPatient)
    {
        Console.WriteLine($"You are logged in as a patient and your name is {loggedInPatient.FirstName} {loggedInPatient.LastName}");
    }
}
