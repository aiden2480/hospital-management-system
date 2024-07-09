using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;

internal class DoctorMenuService(HospitalDbContext db) : IDoctorMenuService
{
    public void MainMenu(Doctor loggedInDoctor)
    {
        Console.WriteLine($"You are logged in as a doctor and your name is {loggedInDoctor.FirstName} {loggedInDoctor.LastName}");
        Console.WriteLine("Your appointments:");

        var appointments = db.Appointments
            .Where(a => a.DoctorId == loggedInDoctor.Id)
            .Include(a => a.Patient)
            .Include(a => a.Doctor);

        foreach (var appt in appointments)
        {
            Console.WriteLine($"{appt.Id} | {appt.Doctor.FullName} | {appt.Patient.FullName} | {appt.ScheduledTime.ToShortDateString()} | {appt.Description}");
        }
    }
}
