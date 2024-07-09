using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;

internal class AdminMenuService(HospitalDbContext db) : IAdminMenuService
{
    public void MainMenu(Administrator loggedInAdmin)
    {
        Console.WriteLine($"You are logged in as an admin and your password is {loggedInAdmin.Password}");
        Console.WriteLine("All appointments:");

        var appointments = db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor);

        foreach (var appt in appointments)
        {
            Console.WriteLine($"{appt.Id} | {appt.Doctor.FullName} | {appt.Patient.FullName} | {appt.ScheduledTime.ToShortDateString()} | {appt.Description}");
        }
    }
}
