namespace HospitalManagementSystem.Entity;

public class Doctor : LoginCapableUserWithDetails
{
    protected override int Offset => 10000;

    public ICollection<Appointment> Appointments { get; } = [];
}

public class Patient : LoginCapableUserWithDetails
{
    protected override int Offset => 20000;

    public ICollection<Appointment> Appointments { get; } = [];
}

public class Administrator : LoginCapableUser
{
    protected override int Offset => 30000;
}

public class Appointment
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public Doctor Doctor { get; } = null!;

    public int PatientId { get; set; }

    public Patient Patient { get; } = null!;

    public required string Description { get; init; }
}
