using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Extension;
using HospitalManagementSystem.Interfaces;
using Spectre.Console;

namespace HospitalManagementSystem.Services;

internal class AdminMenuService(IAppointmentRepository apptRepo, IDoctorRepository doctorRepo, IPatientRepository patientRepo) : AbstractMenuService<Administrator>, IAdminMenuService
{
    protected override string MenuDescription(Administrator admin)
        => $"Welcome to the admin menu, [darkmagenta]{admin.FullName}[/]. The hospital currently has [darkorange]{doctorRepo.GetTotalCount()}[/] doctors and [darkorange]{patientRepo.GetTotalCount()}[/] patients.";

    protected override Dictionary<string, Action<Administrator>> MenuActions => new()
    {
        { "List all doctors", ListAllDoctors },
        { "List doctor details", ListDoctorDetails },
        { "List all patients", ListAllPatients },
        { "List patient details", ListPatientDetails },
        { "List all appointments", ListAllAppointments },
        { "List appointment details", ListAppointmentDetails },
        { "Add doctor", AddDoctor },
        { "Add patient", AddPatient },
    };

    private void ListAllDoctors(Administrator admin)
        => AnsiConsole.Write(doctorRepo.GetAll().ToTable());

    private void ListDoctorDetails(Administrator admin)
        => AnsiConsole.Write(Selector(doctorRepo.GetAll()).ToTable());

    private void ListAllPatients(Administrator admin)
        => AnsiConsole.Write(patientRepo.GetAll().ToTable());

    private void ListPatientDetails(Administrator admin)
        => AnsiConsole.Write(Selector(patientRepo.GetAll()).ToTable());

    private void ListAllAppointments(Administrator admin)
        => AnsiConsole.Write(apptRepo.GetAll().ToTable());

    private void ListAppointmentDetails(Administrator admin)
        => AnsiConsole.Write(Selector(apptRepo.GetAll()).ToTable());

    private void AddDoctor(Administrator admin)
        => doctorRepo.Add(CreateNewDoctor()).SaveChanges();

    private void AddPatient(Administrator aPatientdmin)
        => patientRepo.Add(CreateNewPatient()).SaveChanges();

    private static T Selector<T>(IEnumerable<T> users) where T : AbstractUserWithAppointments
    {
        AnsiConsole.Write(users.ToTable());

        var prompt = new SelectionPrompt<T>()
            .Title("Please choose an option:")
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(users)
            .PageSize(3)
            .UseConverter(d => d.FullName);

        return AnsiConsole.Prompt(prompt);
    }

    private static Appointment Selector(IEnumerable<Appointment> appointments)
    {
        AnsiConsole.Write(appointments.ToTable());

        var prompt = new SelectionPrompt<Appointment>()
            .Title("Please choose an option:")
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(appointments)
            .PageSize(3)
            .UseConverter(a => $"{a.Doctor.FullName} - {a.Patient.FullName} ({a.Id})");

        return AnsiConsole.Prompt(prompt);
    }

    // The following two methods break DRY but I couldn't figure out another way
    // to create the two essentially identical objects without breaking type safety
    private static Doctor CreateNewDoctor()
    {
        AnsiConsole.MarkupLine($"Creating a new [blue]doctor[/]:\n");

        return new Doctor()
        {
            FirstName = ConsoleService.ReadString("First name: "),
            LastName = ConsoleService.ReadString("Last name: "),
            Email = ConsoleService.ReadString("Email: "),
            PhoneNumber = ConsoleService.ReadString("Phone number: "),
            AddrStreetNumber = ConsoleService.ReadString("Street number: "),
            AddrStreet = ConsoleService.ReadString("Street: "),
            AddrCity = ConsoleService.ReadString("City: "),
            AddrState = ConsoleService.ReadString("State: "),
            Password = ConsoleService.ReadPassword("Password: "),
        };
    }

    private static Patient CreateNewPatient()
    {
        AnsiConsole.MarkupLine($"Creating a new [blue]patient[/]:\n");

        return new Patient()
        {
            FirstName = ConsoleService.ReadString("First name: "),
            LastName = ConsoleService.ReadString("Last name: "),
            Email = ConsoleService.ReadString("Email: "),
            PhoneNumber = ConsoleService.ReadString("Phone number: "),
            AddrStreetNumber = ConsoleService.ReadString("Street number: "),
            AddrStreet = ConsoleService.ReadString("Street: "),
            AddrCity = ConsoleService.ReadString("City: "),
            AddrState = ConsoleService.ReadString("State: "),
            Password = ConsoleService.ReadPassword("Password: "),
        };
    }
}
