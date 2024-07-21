using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;

namespace HospitalManagementSystem.Repositories;

internal class AdministratorRepository(HospitalDbContext db) : AbstractRepository<Administrator>(db), IAdministratorRepository
{
}
