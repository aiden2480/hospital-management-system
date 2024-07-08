using Microsoft.EntityFrameworkCore;
using static System.Environment;

namespace HospitalManagementSystem.Entity;

public class HospitalDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Administrator> Administrators { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public IEnumerable<LoginCapableUser> LoginCapableUsers
        => Doctors.AsEnumerable().Cast<LoginCapableUser>()
            .Concat(Patients.Cast<LoginCapableUser>())
            .Concat(Administrators.Cast<LoginCapableUser>());

    public HospitalDbContext()
        => Database.EnsureCreated();

    protected virtual string DatabasePath
        => Path.Join(GetFolderPath(SpecialFolder.LocalApplicationData), "AidenGardnerHMS", "hmsdatabase.db");

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var directory = new FileInfo(DatabasePath).Directory!.FullName;
        Directory.CreateDirectory(directory);

        options.UseSqlite($"Data Source={DatabasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<EntityName>(b =>
        //{
        //    b.ToTable("TabelName");
        //    b.Property(x => x.ColumnName).ValueGeneratedOnAdd().UseIdentityColumn(1000, 1);
        //});

        //modelBuilder
        //    .Entity<Patient>()
        //    .Property(a => a.Id)
        //    .HasComputedColumnSql("Id + 10000");

        modelBuilder.Entity<Administrator>().HasData(new Administrator { Id = 30001, Password = "p@assw0rd" });
    }
}
