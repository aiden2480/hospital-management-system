using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Repositories;
using HospitalManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace HospitalManagementSystem;

internal class Program
{
    static void Main()
    {
        var services = GetServices();

        // This can be wrapped in a while true method because
        // the main menu should call Environment.Exit if needed
        while (true)
        {
            InvokeLoginMenu(services, out var loggedInUser);
            InvokeMainMenu(services, loggedInUser);
        }
    }

    private static ServiceProvider GetServices()
        => new ServiceCollection()
            .AddDbContext<HospitalDbContext>()
            .AddSingleton<ILoginService, LoginService>()
            .AddSingleton<IDoctorMenuService, DoctorMenuService>()
            .AddSingleton<IPatientMenuService, PatientMenuService>()
            .AddSingleton<IAdminMenuService, AdminMenuService>()
            .AddSingleton<IDoctorRepository, DoctorRepository>()
            .AddSingleton<IPatientRepository, PatientRepository>()
            .AddSingleton<IAdministratorRepository, AdministratorRepository>()
            .AddSingleton<IAppointmentRepository, AppointmentRepository>()
            .BuildServiceProvider();

    private static void InvokeLoginMenu(IServiceProvider services, out AbstractUser loggedInUser)
    {
        AbstractUser? attemptedLogin = null;
        var loginService = services.GetRequiredService<ILoginService>();
        var loginFailed = false;

        while (attemptedLogin == null)
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("Welcome to Hospital Management System");
            AnsiConsole.WriteLine("Please enter your credentials to login.");
            AnsiConsole.MarkupLine(loginFailed ? "[maroon]Login failed[/]\n" : "\n");
            AnsiConsole.Write("User ID: ");

            var userId = ConsoleService.ReadInteger();
            var password = ConsoleService.ReadPassword("Password: ");

            attemptedLogin = loginService.AttemptLogin(userId, password);
            loginFailed = true;
        }

        loggedInUser = attemptedLogin;
    }

    private static void InvokeMainMenu(IServiceProvider services, AbstractUser loggedInUser)
    {
        if (loggedInUser is Doctor doctor)
        {
            services.GetRequiredService<IDoctorMenuService>().MainMenu(doctor);
        }
        if (loggedInUser is Patient patient)
        {
            services.GetRequiredService<IPatientMenuService>().MainMenu(patient);
        }
        if (loggedInUser is Administrator admin)
        {
            services.GetRequiredService<IAdminMenuService>().MainMenu(admin);
        }
    }
}
