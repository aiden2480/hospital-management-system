namespace HospitalManagementSystem.Interfaces;

public interface IConsoleService
{
    string ReadString(string prompt);

    string ReadPassword(string prompt);

    string ReadAndHashPassword(string prompt);

    string ReadEmail(string prompt);

    int ReadInteger(string prompt, bool quitOnEsc);

    public DateTime ReadDateTime();

    public string ReadPhoneNumber(string prompt);
}
