using HospitalManagementSystem.Entity;

namespace HospitalManagementSystem.Interfaces;

public interface IRepository<T> where T : class
{
    T? GetById(int id);

    IEnumerable<T> GetAll();

    IEnumerable<T> Filter(Func<T, bool> predicate);

    T? FilterSingle(Func<T, bool> predicate);

    void Add(T entity);

    void Update(T entity);

    void Remove(T entity);

    void SaveChanges();
}

public interface IDoctorRepository : IRepository<Doctor> { }

public interface IPatientRepository : IRepository<Patient> { }

public interface IAppointmentRepository : IRepository<Appointment>
{
    public IEnumerable<Appointment> GetByPatient(Patient patient);

    public IEnumerable<Appointment> GetByDoctor(Doctor doctor);

    public IEnumerable<Appointment> GetByDoctorAndPatient(Doctor doctor, Patient patient);
}

public interface IAdministratorRepository : IRepository<Administrator> { }
