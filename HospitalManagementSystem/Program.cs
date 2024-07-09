using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem;

internal class Program
{
    static void Main(string[] args)
    {
        var services = GetServices();
        var loginService = services.GetRequiredService<ILoginService>();

        LoginCapableUser? loggedInUser = null;

        while (loggedInUser == null)
        {
            Console.Clear();

            Console.Write("User ID: ");
            var userId = ConsoleService.ReadInteger();

            Console.Write("Password: ");
            var password = ConsoleService.ReadPassword();

            loggedInUser = loginService.AttemptLogin(userId, password);
        }

        InvokeMainMenu(services, loggedInUser);
    }

    private static ServiceProvider GetServices()
        => new ServiceCollection()
            .AddDbContext<HospitalDbContext>()
            .AddSingleton<ILoginService, LoginService>()
            .AddSingleton<IDoctorMenuService, DoctorMenuService>()
            .AddSingleton<IPatientMenuService, PatientMenuService>()
            .AddSingleton<IAdminMenuService, AdminMenuService>()
            .BuildServiceProvider();

    private static void InvokeMainMenu(IServiceProvider services, LoginCapableUser loggedInUser)
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
