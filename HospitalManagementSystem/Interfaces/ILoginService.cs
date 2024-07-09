using HospitalManagementSystem.Entity;

namespace HospitalManagementSystem.Interfaces;

public interface ILoginService
{
    public LoginCapableUser? AttemptLogin(int userId, string password);
}
