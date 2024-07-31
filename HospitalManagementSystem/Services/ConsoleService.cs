using HospitalManagementSystem.Interfaces;
using Spectre.Console;
using System.Text.RegularExpressions;
using static System.ConsoleKey;

namespace HospitalManagementSystem.Services;

internal partial class ConsoleService(IPasswordService passwordService) : IConsoleService
{
    public string ReadString(string prompt)
        => AnsiConsole.Ask<string>(prompt);

    public string ReadString(string prompt, int length)
        => AnsiConsole.Prompt(new TextPrompt<string>(prompt)
            .Validate(s => s.Length <= length)
            .ValidationErrorMessage($"[red]Input must be of length less than or equal to {length}[/]"));

    public string ReadPassword(string prompt)
        => AnsiConsole.Prompt(new TextPrompt<string>(prompt)
            .PromptStyle("grey50")
            .Secret());

    public string ReadAndHashPassword(string prmopt)
        => passwordService.HashPassword(ReadPassword(prmopt));

    public int ReadInteger(string prompt, bool quitOnEsc = false)
    {
        AnsiConsole.Markup(prompt);

        var input = ReadConsole((k, s) =>
        {
            if (quitOnEsc && k.Key == Escape)
            {
                Environment.Exit(0);
            }

            return char.IsDigit(k.KeyChar) && s.Length < 9;
        }, allowEmpty: false);

        return int.Parse(input);
    }

    public DateTime ReadDateTime()
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

    public string ReadEmail(string prompt)
        => AnsiConsole.Prompt(new TextPrompt<string>(prompt)
            .Validate(EmailRegex.IsMatch)
            .ValidationErrorMessage("[red]Input must match email format[/]"));

    public string ReadPhoneNumber(string prompt)
        => AnsiConsole.Prompt(new TextPrompt<string>(prompt)
            .Validate(PhoneNumberRegex.IsMatch)
            .ValidationErrorMessage("[red]Input must match phone number format[/]"));

    public static Table TitleBox(string menuName)
        => new Table()
            .AddColumn("Dotnet Hospital Management System")
            .AddRow(new Markup(menuName).Centered())
            .Centered();

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

    // Regex
    private readonly Regex EmailRegex = MakeEmailRegex();

    private readonly Regex PhoneNumberRegex = MakePhoneNumberRegex();

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex MakeEmailRegex();

    [GeneratedRegex(@"04\d{8}")]
    private static partial Regex MakePhoneNumberRegex();
}
