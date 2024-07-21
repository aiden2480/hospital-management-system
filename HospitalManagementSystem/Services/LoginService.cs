using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Services;

public class LoginService(IAdministratorRepository adminRepo, IDoctorRepository doctorRepo, IPatientRepository patientRepo) : ILoginService
{
    public AbstractUser? AttemptLogin(int userId, string password)
    {
        AbstractUser? user = null;

        user ??= patientRepo.FilterSingle(p => p.Id == userId && p.Password == password);
        user ??= doctorRepo.FilterSingle(d => d.Id == userId && d.Password == password);
        user ??= adminRepo.FilterSingle(a => a.Id == userId && a.Password == password);

        return user;
    }
}
