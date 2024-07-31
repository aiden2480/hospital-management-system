using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Entity;

/// <summary>
/// Any user which can login to the system. This includes patients, doctors, and administrators.
/// </summary>
public abstract class AbstractUser
{
    public int Id { get; set; }

    [StringLength(30)]
    public required string FirstName { get; init; }

    [StringLength(30)]
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
    [EmailAddress]
    public required string Email { get; init; }

    [StringLength(10, MinimumLength = 10)]
    public required string PhoneNumber { get; init; }

    [StringLength(10)]
    public required string AddrStreetNumber { get; init; }

    [StringLength(30)]
    public required string AddrStreet { get; init; }

    [StringLength(30)]
    public required string AddrCity { get; init; }

    [StringLength(15)]
    public required string AddrState { get; init; }

    public ICollection<Appointment> Appointments { get; } = [];

    [NotMapped]
    public string FullAddr => $"{AddrStreetNumber} {AddrStreet}, {AddrCity}, {AddrState}";
}
