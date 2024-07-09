using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Services;

internal class AdminMenuService(HospitalDbContext db) : IAdminMenuService
{
    public void MainMenu(Administrator loggedInAdmin)
    {
        Console.WriteLine($"You are logged in as an admin and your password is {loggedInAdmin.Password}");
    }
}
