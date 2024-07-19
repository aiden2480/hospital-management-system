using HospitalManagementSystem.Entity;
using Spectre.Console;

namespace HospitalManagementSystem.Extension;

static internal class PluralExtensions
{
    public static Table ToTable(this IEnumerable<LoginCapableUserWithDetails> users)
    {
        var table = new Table()
            .AddColumn("Full Name")
            .AddColumn("Email")
            .AddColumn("Phone")
            .AddColumn("Address");

        foreach (var user in users)
        {
            table.AddRow(user.FullName, user.Email, user.PhoneNumber, user.FullAddr);
        }

        return table;
    }

    public static Table ToTable(this IEnumerable<Appointment> appointments)
    {
        var table = new Table()
            .AddColumn("Appointment ID")
            .AddColumn("Doctor")
            .AddColumn("Patient")
            .AddColumn("Time")
            .AddColumn("Description");

        foreach (var a in appointments)
        {
            table.AddRow(a.Id.ToString(), a.Doctor.FullName, a.Patient.FullName, a.ScheduledTime.ToString(), a.Description);
        }

        return table;
    }
}
