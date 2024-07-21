using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Extension;
using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HospitalManagementSystem.Services;

internal class DoctorMenuService(IAppointmentRepository apptRepo) : AbstractMenuService<Doctor>, IDoctorMenuService
{
    protected override string MenuName
        => "Doctor menu";

    protected override string MenuDescription(Doctor doctor)
        => $"Welcome to the doctor menu, {doctor.FullName}";

    protected override Dictionary<string, Action<Doctor>> MenuActions => new()
    {
        { "List my details", ListDoctorDetails },
        { "List my patients", ListPatients },
        { "List my appointments", ListAppointments },
        { "Check particular patient", CheckParticularPatient },
        { "List appointments with patient", CheckAppointmentsWithPatient },
    };

    private void ListDoctorDetails(Doctor doctor)
        => AnsiConsole.Write(doctor.ToTable());

    private void ListPatients(Doctor doctor)
        => AnsiConsole.Write(doctor.Patients.ToTable());

    private void ListAppointments(Doctor doctor)
        => AnsiConsole.Write(doctor.Appointments.ToTable());

    private void CheckParticularPatient(Doctor doctor)
    {
        if (doctor.Patients.Count == 0)
        {
            AnsiConsole.WriteLine("You don't have any patients");
            return;
        }

        var patient = PatientSelector(doctor.Patients);

        AnsiConsole.WriteLine($"\nPatient details for {patient.FullName} are as follows:");
        AnsiConsole.Write(patient.ToTable());
    }

    private void CheckAppointmentsWithPatient(Doctor doctor)
    {
        if (doctor.Patients.Count == 0)
        {
            AnsiConsole.WriteLine("You don't have any patients");
            return;
        }

        var patient = PatientSelector(doctor.Patients);
        var appts = apptRepo.GetByDoctorAndPatient(doctor, patient);

        AnsiConsole.WriteLine($"\nYour appointments with {patient.FullName} are as follows:");
        AnsiConsole.Write(appts.ToTable());
    }

    private static Patient PatientSelector(IEnumerable<Patient> patients)
    {
        AnsiConsole.Write(patients.ToTable());

        var prompt = new SelectionPrompt<Patient>()
            .Title("Please choose a patient of yours:")
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(patients)
            .PageSize(3)
            .UseConverter(p => p.FullName);

        return AnsiConsole.Prompt(prompt);
    }
}
