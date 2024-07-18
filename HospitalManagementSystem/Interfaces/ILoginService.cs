using HospitalManagementSystem.Entity;

namespace HospitalManagementSystem.Interfaces;

public interface ILoginService
{
    /// <summary>
    /// Attempts to log the user into the application with the given user ID and password
    /// </summary>
    /// <param name="userId">User ID. Can be doctor, patient, admin</param>
    /// <param name="password">The password for the account</param>
    /// <returns>The requested user, if the credentials were correct, otherwise null</returns>
    public LoginCapableUser? AttemptLogin(int userId, string password);
}
