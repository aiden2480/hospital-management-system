using Spectre.Console;
using static System.ConsoleKey;

namespace HospitalManagementSystem.Services;

public static class ConsoleService
{
    public static string ReadString(string prompt)
    {
        AnsiConsole.Markup(prompt);
        return Console.ReadLine() ?? "";
    }

    public static string ReadPassword(string prompt)
    {
        var textPrompt = new TextPrompt<string>(prompt)
            .PromptStyle("grey50")
            .Secret();

        return AnsiConsole.Prompt(textPrompt);
    }

    public static int ReadInteger()
    {
        var input = ReadConsole((k, s) => char.IsDigit(k.KeyChar), allowEmpty: false);
        return int.Parse(input);
    }

    public static DateTime ReadDateTime()
    {
        var prompt = new TextPrompt<string>("Enter a date and time (e.g. 2024-07-19 14:30):")
            .PromptStyle("green")
            .Validate(input =>
            {
                return DateTime.TryParse(input, out _)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Invalid date and time format.[/]");
            });

        var input = AnsiConsole.Prompt(prompt);
        return DateTime.Parse(input);
    }

    private static string ReadConsole(Func<ConsoleKeyInfo, string, bool> allowKey, bool allowEmpty)
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
            Console.Write(keyInfo.KeyChar);
            input += keyInfo.KeyChar;
        }
    }
}
