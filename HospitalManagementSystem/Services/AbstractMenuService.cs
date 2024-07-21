using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using Spectre.Console;

namespace HospitalManagementSystem.Services;

public abstract class AbstractMenuService<T> : IMenuService<T> where T : AbstractUser
{
    protected abstract string MenuDescription(T loggedInUser);

    protected abstract Dictionary<string, Action<T>> MenuActions { get; }

    public void MainMenu(T loggedInUser)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(ConsoleService.TitleBox($"{typeof(T).Name} Menu"));

        var selection = GetSelection(loggedInUser);

        if (selection == "Exit to login")
        {
            return;
        }
        else if (selection == "Exit to system")
        {
            Environment.Exit(0);
        }
        else
        {
            MenuActions[selection].Invoke(loggedInUser);
            WaitForUser();
            MainMenu(loggedInUser);
        }
    }

    private string GetSelection(T loggedInUser)
    {
        var prompt = new SelectionPrompt<string>()
            .Title($"{MenuDescription(loggedInUser)}\n\nPlease choose an option:")
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(MenuActions.Keys)
            .AddChoices("Exit to login", "Exit to system");

        return AnsiConsole.Prompt(prompt);
    }

    private static void WaitForUser()
    {
        AnsiConsole.Markup("\n[grey]Press enter to continue...[/]");
        Console.ReadKey(true);
    }
}
