using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Repositories;
using HospitalManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Configuration;

namespace HospitalManagementSystem;

internal class Program
{
    static void Main()
    {
        EnsureRequiredSettingsSet();
        var services = GetServices();
        var db = services.GetRequiredService<HospitalDbContext>();
        db.Database.EnsureCreated();

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
            .AddSingleton<IEmailService, EmailService>()
            .AddSingleton<IConsoleService, ConsoleService>()
            .AddSingleton<IPasswordService, PasswordService>()
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
        var loginFailed = false;
        var loginService = services.GetRequiredService<ILoginService>();
        var consoleService = services.GetRequiredService<IConsoleService>();

        while (attemptedLogin == null)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(ConsoleService.TitleBox("Login Menu"));
            AnsiConsole.MarkupLine("Please enter your credentials to login, or press [grey]esc[/] to quit.");
            AnsiConsole.MarkupLine(loginFailed ? "[maroon]Invalid credentials, please try again.[/]\n" : "\n");

            var userId = consoleService.ReadInteger("User ID: ", quitOnEsc: true);
            var password = consoleService.ReadPassword("Password: ");

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

    private static void EnsureRequiredSettingsSet()
    {
        static void NullCheck(string name)
            => ArgumentNullException.ThrowIfNull(ConfigurationManager.AppSettings[name], name);

        NullCheck("SmtpHost");
        NullCheck("SmtpPort");
        NullCheck("SmtpUser");
        NullCheck("SmtpPass");
        NullCheck("SmtpEnableSsl");
    }
}
