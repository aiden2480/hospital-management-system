using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Repositories;

internal class AppointmentRepository(HospitalDbContext db) : AbstractRepository<Appointment>(db), IAppointmentRepository
{
    public IEnumerable<Appointment> GetByPatient(Patient patient)
        => from appt in _entities
           where appt.Patient == patient
           select appt;

    public IEnumerable<Appointment> GetByDoctor(Doctor doctor)
        => from appt in _entities
           where appt.Doctor == doctor
           select appt;

    public IEnumerable<Appointment> GetByDoctorAndPatient(Doctor doctor, Patient patient)
        => from appt in _entities
           where appt.Doctor == doctor
           where appt.Patient == patient
           select appt;
}
