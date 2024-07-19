using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Extension;
using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HospitalManagementSystem.Services;

internal class PatientMenuService(HospitalDbContext db) : AbstractMenuService<Patient>, IPatientMenuService
{
    protected override string MenuName
        => "Patient menu";

    protected override string MenuDescription(Patient patient)
        => $"Welcome to the patient menu, {patient.FullName}";

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
    {
        var table = db.Appointments
            .Where(a => a.PatientId == patient.Id)
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ToTable();

        AnsiConsole.Write(table);
    }

    private void BookAppointment(Patient patient)
    {
        if (patient.Doctor is null)
        {
            AssignDoctor(patient);
        }

        var description = ConsoleService.ReadString("Enter description: ");//AnsiConsole.Markup("Enter appointment description: ");
        var apptTime = ConsoleService.ReadDateTime();

        var appt = new Appointment
        {
            Patient = patient,
            Doctor = patient.Doctor!,
            ScheduledTime = apptTime,
            Description = description
        };

        db.Appointments.Add(appt);
        db.SaveChanges();

        AnsiConsole.MarkupLine("[green]Appointment booked successfully[/]");
    }

    private void AssignDoctor(Patient patient)
    {
        var doctors = db.Doctors.ToList();

        AnsiConsole.WriteLine("You do not have an assigned doctor. Please choose one now");
        AnsiConsole.Write(doctors.ToTable());

        var prompt = new SelectionPrompt<Doctor>()
            .Title("Please choose an option:")
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(doctors)
            .PageSize(3)
            .UseConverter(d => d.FullName);

        patient.Doctor = AnsiConsole.Prompt(prompt);
        db.SaveChanges();
    }
}
