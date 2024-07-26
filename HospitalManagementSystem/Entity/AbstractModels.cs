using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Entity;

/// <summary>
/// Any user which can login to the system. This includes patients, doctors, and administrators.
/// </summary>
public abstract class AbstractUser
{
    public int Id { get; set; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string PasswordHash { get; init; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}

/// <summary>
/// Any user with details which can login to the system. This includes patients and doctors but
/// not administrators.
/// </summary>
public abstract class AbstractUserWithAppointments : AbstractUser
{
    public required string Email { get; init; }

    public required string PhoneNumber { get; init; }

    public required string AddrStreetNumber { get; init; }

    public required string AddrStreet { get; init; }

    public required string AddrCity { get; init; }

    public required string AddrState { get; init; }

    public ICollection<Appointment> Appointments { get; } = [];

    [NotMapped]
    public string FullAddr => $"{AddrStreetNumber} {AddrStreet}, {AddrCity}, {AddrState}";
}
