using HospitalManagementSystem.Entity;
using Spectre.Console;

namespace HospitalManagementSystem.Extension
{
    static internal class SingularExtensions
    {
        public static Table ToTable(this LoginCapableUserWithDetails user)
        {
            var type = user.GetType().Name;
            var table = new Table()
                .AddColumns("", "")
                .AddRow($"{type} ID", user.Id.ToString())
                .AddRow("Full name", user.FullName)
                .AddRow("Address", user.FullAddr)
                .AddRow("Email", user.Email)
                .AddRow("Phone", user.PhoneNumber)
                .HideHeaders();

            return table;
        }
    }
}
