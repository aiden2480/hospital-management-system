using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Repositories;

internal class PatientRepository(HospitalDbContext db) : AbstractRepository<Patient>(db), IPatientRepository
{
}
