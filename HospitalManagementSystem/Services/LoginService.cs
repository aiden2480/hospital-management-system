using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Services;

public class LoginService(IAdministratorRepository adminRepo, IDoctorRepository doctorRepo, IPasswordService passwordService, IPatientRepository patientRepo) : ILoginService
{
    public AbstractUser? AttemptLogin(int userId, string password)
    {
        // First we check if the ID exists
        var allUsers = adminRepo.GetAll().Cast<AbstractUser>()
            .Concat(doctorRepo.GetAll().Cast<AbstractUser>())
            .Concat(patientRepo.GetAll().Cast<AbstractUser>());

        var user = allUsers.SingleOrDefault(u => u.Id == userId);

        // If it exists, check the password hash is the same
        if (user != null && passwordService.ValidatePassword(password, user.PasswordHash))
        {
            return user;
        }

        return null;
    }
}
