using HospitalManagementSystem.Entity;
using Spectre.Console;

namespace HospitalManagementSystem.Extension
{
    static internal class SingularExtensions
    {
        public static Table ToTable(this AbstractUserWithAppointments user)
            => new Table()
                .AddColumns("", "")
                .AddRow($"{user.GetType().Name} ID", user.Id.ToString())
                .AddRow("Full name", user.FullName)
                .AddRow("Address", user.FullAddr)
                .AddRow("Email", user.Email)
                .AddRow("Phone", user.PhoneNumber)
                .HideHeaders();

        public static Table ToTable(this Appointment appointment)
            => new Table()
                .AddColumns("", "")
                .AddRow("Appointment ID", appointment.Id.ToString())
                .AddRow("Doctor", appointment.Doctor.FullName)
                .AddRow("Patient", appointment.Patient.FullName)
                .AddRow("Time", appointment.ScheduledTime.ToString())
                .AddRow("Description", appointment.Description)
                .HideHeaders();
    }
}
