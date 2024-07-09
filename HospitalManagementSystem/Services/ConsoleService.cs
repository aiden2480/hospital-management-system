using static System.ConsoleKey;

namespace HospitalManagementSystem.Services;

public static class ConsoleService
{
    public static string ReadPassword()
        => ReadConsole(maskInput: true, allowEmpty: false);

    public static int ReadInteger()
    {
        var input = ReadConsole((k, s) => char.IsDigit(k.KeyChar), maskInput: false, allowEmpty: false);
        return int.Parse(input);
    }

    private static string ReadConsole(Func<ConsoleKeyInfo, string, bool> allowKey, bool maskInput, bool allowEmpty)
    {
        ConsoleKeyInfo keyInfo;
        var input = string.Empty;

        while (true)
        {
            keyInfo = Console.ReadKey(true);

            // If enter key pressed, return input value. We must check
            // if empty input is allowed.
            if (keyInfo.Key == Enter)
            {
                if (input.Length == 0 && !allowEmpty)
                {
                    continue;
                }

                Console.WriteLine();
                return input;
            }
            
            // Remove the last character from input and delete from console
            if (keyInfo.Key == Backspace)
            {
                if (input.Length > 0)
                {
                    input = input[..^1];
                    Console.Write("\b \b");
                }

                continue;
            }

            // Is this key allowed to be pressed
            if (!allowKey.Invoke(keyInfo, input))
            {
                continue;
            }

            // Print asterisks if we should mask the key input
            Console.Write(maskInput ? "*" : keyInfo.KeyChar);
            input += keyInfo.KeyChar;
        }
    }

    private static string ReadConsole(bool maskInput, bool allowEmpty)
        => ReadConsole((_, _) => true, maskInput, allowEmpty);
}
