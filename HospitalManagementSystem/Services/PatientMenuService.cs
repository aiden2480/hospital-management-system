using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;

internal class PatientMenuService(HospitalDbContext db) : IPatientMenuService
{
    public void MainMenu(Patient loggedInPatient)
    {
        Console.WriteLine($"You are logged in as a patient and your name is {loggedInPatient.FirstName} {loggedInPatient.LastName}");
        Console.WriteLine("Your appointments:");

        var appointments = db.Appointments
            .Where(a => a.PatientId == loggedInPatient.Id)
            .Include(a => a.Patient)
            .Include(a => a.Doctor);

        foreach (var appt in appointments)
        {
            Console.WriteLine($"{appt.Id} | {appt.Doctor.FullName} | {appt.Patient.FullName} | {appt.ScheduledTime.ToShortDateString()} | {appt.Description}");
        }
    }
}
