using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using System.Net;
using System.Net.Mail;
using static System.Configuration.ConfigurationManager;
using static System.Web.HttpUtility;

namespace HospitalManagementSystem.Services;

internal class EmailService : IEmailService
{
    public void SendEmail(string toEmail, string subject, string body)
    {
        using var client = GetClient();
        using var message = new MailMessage()
        {
            From = new MailAddress(FromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(toEmail));

        client.Send(message);
    }

    public void SendAppointmentConfirmation(Appointment appointment)
    {
        var apptTableHtml = $@"
<style>
    td:first-child {{ font-weight: bold }}
</style>
<table>
    <tr>
        <td>Appointment ID</td>
        <td>{appointment.Id}</td>
    </tr>
    <tr>
        <td>Doctor</td>
        <td>{HtmlEncode(appointment.Doctor.FullName)}</td>
    </tr>
    <tr>
        <td>Patient</td>
        <td>{HtmlEncode(appointment.Patient.FullName)}</td>
    </tr>
    <tr>
        <td>Time</td>
        <td>{appointment.ScheduledTime}</td>
    </tr>
    <tr>
        <td>Description</td>
        <td>{HtmlEncode(appointment.Description)}</td>
    </tr>
</table>
";

        using var client = GetClient();
        using var message = new MailMessage()
        {
            From = new MailAddress(FromEmail, "Hospital Management System"),
            Subject = "New Appointment",
            Body = $"Hi,\n\nYou have a new appointment. Details are as follows:\n\n{apptTableHtml}",
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(appointment.Doctor.Email, appointment.Doctor.FullName));
        message.To.Add(new MailAddress(appointment.Patient.Email, appointment.Patient.FullName));

        client.Send(message);
    }

    public Task SendAppointmentConfirmationAsync(Appointment appointment)
        => Task.Run(() => SendAppointmentConfirmation(appointment));

    private readonly string FromEmail = AppSettings["SmtpUser"]!;

    private SmtpClient GetClient() => new()
    {
        Host = AppSettings["SmtpHost"]!,
        Port = int.Parse(AppSettings["SmtpPort"]!),
        Credentials = new NetworkCredential(FromEmail, AppSettings["SmtpPass"]),
        EnableSsl = bool.Parse(AppSettings["SmtpEnableSsl"]!)
    };
}
