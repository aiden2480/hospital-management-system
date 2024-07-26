namespace HospitalManagementSystem.Interfaces;

public interface IPasswordService
{
    /// <summary>
    /// Hash and salt a password then return the Base64 encoded hash
    /// </summary>
    /// <param name="password">The password to be hashed</param>
    /// <returns>A Base64 encoded version of the hashed and salted password</returns>
    string HashPassword(string password);

    /// <summary>
    /// Validate a password against a stored password hash
    /// </summary>
    /// <param name="password">The password input from the user</param>
    /// <param name="storedPasswordAndSaltHash">The hashed password stored in the database</param>
    /// <returns>A bool indicating if the password was correct</returns>
    bool ValidatePassword(string password, string storedPasswordAndSaltHash);
}
