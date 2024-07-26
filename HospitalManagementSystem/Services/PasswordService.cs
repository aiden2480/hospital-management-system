using HospitalManagementSystem.Interfaces;
using System.Security.Cryptography;

namespace HospitalManagementSystem.Services;

internal class PasswordService : IPasswordService
{
    /* Changing these values will invalidate all previous
     * passwords and probably break the app
     */

    // Number of times to hash the password with the salt
    private const int HashIterations = 1200;

    // Length in bytes of the password hash
    private const int KeyLength = 32;

    // Length in bytes of the salt hash
    private const int SaltLength = 16; 

    public string HashPassword(string password)
        => HashPassword(password, GenerateRandomSalt());

    /// <summary>
    /// Hash a password using a given salt. This is used to verify the
    /// user input password is correct. 
    /// </summary>
    private static string HashPassword(string password, byte[] salt)
    {
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HashIterations, HashAlgorithmName.SHA256);
        var passwordHash = pbkdf2.GetBytes(KeyLength);

        return CombineToBase64(passwordHash, salt);
    }

    public bool ValidatePassword(string password, string storedPasswordAndSaltHash)
    {
        var salt = GetSaltFromHashedPassword(storedPasswordAndSaltHash);
        var newHash = HashPassword(password, salt);

        // Vulnerable to timing attacks? Probably
        return newHash == storedPasswordAndSaltHash;
    }

    private static byte[] GenerateRandomSalt()
        => RandomNumberGenerator.GetBytes(SaltLength);

    // The salt is the last SaltLength bytes in the password hash
    private static byte[] GetSaltFromHashedPassword(byte[] passwordAndSaltHash)
        => new ArraySegment<byte>(passwordAndSaltHash, KeyLength, SaltLength).ToArray();

    private static byte[] GetSaltFromHashedPassword(string passwordAndSaltHash)
        => GetSaltFromHashedPassword(Convert.FromBase64String(passwordAndSaltHash));

    // Concatenate the password hash
    private static string CombineToBase64(byte[] passwordOnlyHash, byte[] salt)
    {
        var passwordAndSaltHash = new byte[KeyLength + SaltLength];

        Array.Copy(passwordOnlyHash, 0, passwordAndSaltHash, 0, KeyLength);
        Array.Copy(salt, 0, passwordAndSaltHash, KeyLength, SaltLength);

        return Convert.ToBase64String(passwordAndSaltHash);
    }
}
