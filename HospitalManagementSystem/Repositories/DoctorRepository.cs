using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Repositories;

internal class DoctorRepository(HospitalDbContext db) : AbstractRepository<Doctor>(db), IDoctorRepository
{
}
