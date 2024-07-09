using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Services;

public class LoginService(HospitalDbContext db) : ILoginService
{
    public LoginCapableUser? AttemptLogin(int userId, string password)
        => db.LoginCapableUsers.FirstOrDefault(u => u.Id == userId && u.Password == password);
}
