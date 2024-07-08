using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Entity;

/// <summary>
/// Any user which can login to the system. This includes patients, doctors, and administrators.
/// </summary>
[Index(nameof(IdGenerated), IsUnique = true)]
public abstract class LoginCapableUser
{
    [Key]
    private int IdGenerated { get; set; }

    public int Id { get => IdGenerated + Offset; set => IdGenerated = value - Offset; }

    public required string Password { get; init; }

    protected abstract int Offset { get; }
}

/// <summary>
/// Any user with details which can login to the system. This includes patients and doctors but
/// not administrators.
/// </summary>
public abstract class LoginCapableUserWithDetails : LoginCapableUser
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }

    public required string PhoneNumber { get; init; }

    public required string AddrStreetNumber { get; init; }

    public required string AddrStreet {  get; init; }

    public required string AddrCity { get; init; }

    public required string AddrState { get; init; }
}
