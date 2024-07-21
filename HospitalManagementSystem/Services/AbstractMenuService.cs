using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using Spectre.Console;

namespace HospitalManagementSystem.Services;

public abstract class AbstractMenuService<T> : IMenuService<T> where T : AbstractUser
{
    protected abstract string MenuName { get; }

    protected abstract string MenuDescription(T loggedInUser);

    protected abstract Dictionary<string, Action<T>> MenuActions { get; }

    public void MainMenu(T loggedInUser)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .AddColumn("Dotnet Hospital Management System")
            .AddRow(MenuName)
            .Centered();

        AnsiConsole.Write(table);

        //AnsiConsole.Write(new Panel(table)
        //    //.Expand()
        //    .Border(BoxBorder.Rounded)
        //    .Header("Table header");
        //AnsiConsole.WriteLine(MenuDescription(loggedInUser));

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please choose an option:")
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(MenuActions.Keys)
                .AddChoices("Exit to login", "Exit to system"));

        if (selection == "Exit to login")
        {
            return;
        }
        else if (selection == "Exit to system")
        {
            Environment.Exit(0);
        }

        var method = MenuActions[selection];
        method.Invoke(loggedInUser);

        WaitForUser();
        MainMenu(loggedInUser);
    }

    private static void WaitForUser()
    {
        AnsiConsole.Markup("\n[grey]Press enter to continue...[/]");
        Console.ReadKey(true);
    }
}
