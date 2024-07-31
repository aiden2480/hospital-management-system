using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Extension;
using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HospitalManagementSystem.Services;

internal class PatientMenuService(IAppointmentRepository apptRepo, IConsoleService consoleService, IDoctorRepository doctorRepo, IEmailService emailService) : AbstractMenuService<Patient>, IPatientMenuService
{
    protected override string MenuDescription(Patient patient)
        => $"Welcome to the patient menu, [darkmagenta]{patient.FullName}[/]. You currently have [darkorange]{patient.Appointments.Count}[/] appointments.";

    protected override Dictionary<string, Action<Patient>> MenuActions => new()
    {
        { "List my details", ListPatientDetails },
        { "List my doctor's details", ListDoctorDetails },
        { "List my appointments", ListPatientAppointments },
        { "Book appointment", BookAppointment },
    };

    private void ListPatientDetails(Patient patient)
    {
        AnsiConsole.WriteLine("Your details are as follows:");
        AnsiConsole.Write(patient.ToTable());
    }

    private void ListDoctorDetails(Patient patient)
    {
        if (patient.Doctor is null)
        {
            AssignDoctor(patient);
        }

        AnsiConsole.WriteLine("Your doctor's details are as follows:");
        AnsiConsole.Write(patient.Doctor!.ToTable());
    }

    private void ListPatientAppointments(Patient patient)
        => AnsiConsole.Write(patient.Appointments.ToTable());

    private void BookAppointment(Patient patient)
    {
        if (patient.Doctor is null)
        {
            AssignDoctor(patient);
        }

        var description = consoleService.ReadString("Enter appointment description: ");
        var apptTime = consoleService.ReadDateTime();

        var appt = new Appointment
        {
            Patient = patient,
            Doctor = patient.Doctor!,
            ScheduledTime = apptTime,
            Description = description
        };

        apptRepo.Add(appt).SaveChanges();
        emailService.SendAppointmentConfirmationAsync(appt);

        AnsiConsole.MarkupLine("[green]Appointment booked successfully[/]");
    }

    private void AssignDoctor(Patient patient)
    {
        var allDoctors = doctorRepo
            .GetAll()
            .Where(p => p != null)
            .ToList();

        AnsiConsole.WriteLine("You do not have an assigned doctor. Please choose one now");
        AnsiConsole.Write(allDoctors.ToTable());

        var prompt = new SelectionPrompt<Doctor>()
            .Title("Please choose an option:")
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(allDoctors)
            .PageSize(3)
            .UseConverter(d => d.FullName);

        patient.Doctor = AnsiConsole.Prompt(prompt);
        doctorRepo.SaveChanges();
    }
}
