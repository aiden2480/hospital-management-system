using HospitalManagementSystem.Entity;

namespace HospitalManagementSystem.Interfaces;

internal interface IEmailService
{
    void SendEmail(string toEmail, string subject, string body);

    void SendAppointmentConfirmation(Appointment appointment);
}
