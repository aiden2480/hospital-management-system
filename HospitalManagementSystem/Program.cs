using HospitalManagementSystem.Entity;

namespace HospitalManagementSystem;

internal class Program
{
    static void Main(string[] args)
    {
        using var db = new HospitalDbContext();

        var patient = db.Patients.FirstOrDefault(e => e.FirstName == "Patricia");
        var doctor = db.Doctors.FirstOrDefault(e => e.FirstName == "Brandon");
        var admin = db.Administrators.FirstOrDefault(e => e.Id == 30001);
        var appt = db.Appointments.FirstOrDefault(e => e.Description == "sample");

        patient ??= db.Patients.Add(new Patient
        {
            FirstName = "Patricia",
            LastName = "Blanc",
            Email = "patblanc@gmail.com",
            PhoneNumber = "1234567890",
            AddrStreetNumber = "8",
            AddrStreet = "Blake Street",
            AddrCity = "Sydney",
            AddrState = "NSW",
            Password = "password",
        }).Entity;

        doctor ??= db.Doctors.Add(new Doctor
        {
            FirstName = "Brandon",
            LastName = "Blake",
            Email = "bblake@hospital.com",
            PhoneNumber = "1234567890",
            AddrStreetNumber = "1",
            AddrStreet = "Viola Lane",
            AddrCity = "Brisbane",
            AddrState = "VIC",
            Password = "password",
        }).Entity;

        admin ??= db.Administrators.Add(new Administrator
        {
            Password = "test password",
        }).Entity;

        db.SaveChanges();

        appt ??= db.Appointments.Add(new Appointment
        {
            DoctorId = doctor.Id,
            PatientId = patient.Id,
            Description = "sample",
            ScheduledTime = DateTime.Now,
        }).Entity;

        db.SaveChanges();
    }
}
