namespace HospitalManagementSystem.Entity;

/// <summary>
/// A doctor with scheduled appointments, capable of logging into the system.
/// </summary>
public class Doctor : LoginCapableUserWithDetails
{
    public ICollection<Appointment> Appointments { get; } = [];
}

/// <summary>
/// A patient with scheduled appointments, capable of logging into the system.
/// </summary>
public class Patient : LoginCapableUserWithDetails
{
    public ICollection<Appointment> Appointments { get; } = [];
}

/// <summary>
/// An administrator of the system, capable of performing any action on behalf
/// of doctors or patients
/// </summary>
public class Administrator : LoginCapableUser { }

/// <summary>
/// An appointment between a patient and a doctor.
/// </summary>
public class Appointment
{
    public int Id { get; }

    public int DoctorId { get; init; }

    public Doctor Doctor { get; } = null!;

    public int PatientId { get; init; }

    public Patient Patient { get; } = null!;

    public required string Description { get; init; }

    public required DateTime ScheduledTime { get; init; }
}
