using HospitalManagementSystem.Entity;

namespace HospitalManagementSystem.Interfaces;

public interface IRepository<T> where T : class
{
    T? GetById(int id);

    IEnumerable<T> GetAll();

    IEnumerable<T> Filter(Func<T, bool> predicate);

    T? FilterSingle(Func<T, bool> predicate);

    int GetTotalCount();

    IRepository<T> Add(T entity);

    IRepository<T> Update(T entity);

    IRepository<T> Remove(T entity);

    void SaveChanges();
}

public interface IDoctorRepository : IRepository<Doctor> { }

public interface IPatientRepository : IRepository<Patient> { }

public interface IAppointmentRepository : IRepository<Appointment>
{
    public IEnumerable<Appointment> GetByDoctorAndPatient(Doctor doctor, Patient patient);
}

public interface IAdministratorRepository : IRepository<Administrator> { }
