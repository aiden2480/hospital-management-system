using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Services;

public class LoginService(IPatientRepository patientRepo, IDoctorRepository doctorRepo, IAdministratorRepository adminRepo) : ILoginService
{
    public LoginCapableUser? AttemptLogin(int userId, string password)
    {
        LoginCapableUser? user = null;

        user ??= patientRepo.FilterSingle(p => p.Id == userId && p.Password == password);
        user ??= doctorRepo.FilterSingle(d => d.Id == userId && d.Password == password);
        user ??= adminRepo.FilterSingle(a => a.Id == userId && a.Password == password);

        return user;
    }
}
