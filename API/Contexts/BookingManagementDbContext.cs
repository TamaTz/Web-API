using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts;

public class BookingManagementDbContext : DbContext
{
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) { }

    // This is where we define the tables in the database.
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    // This is where we define the relationships between the tables in the database.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>().HasIndex(e => new {
            e.Nik,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        // Relation between Education and University (1 to Many)
        builder.Entity<Education>()
               .HasOne(u => u.University)
               .WithMany(e => e.Educations)
               .HasForeignKey(e => e.UniversityGuid);

        // Relation between Education and Employee (1 to 1)
        builder.Entity<Education>()
               .HasOne(e => e.Employee)
               .WithOne(e => e.Education)
               .HasForeignKey<Education>(e => e.Guid);

        // Relation between Account and Employee (1 to 1)
        builder.Entity<Account>()
               .HasOne(a => a.Employee)
               .WithOne(e => e.Account)
               .HasForeignKey<Account>(a => a.Guid);

        // Relation between Account and AccountRole (1 to Many)
        builder.Entity<AccountRole>()
               .HasOne(a => a.Account)
               .WithMany(a => a.AccountRoles)
               .HasForeignKey(a => a.AccountGuid);

        // Relation between Role and AccountRole (1 to Many)
        builder.Entity<AccountRole>()
               .HasOne(a => a.Role)
               .WithMany(r => r.AccountRoles)
               .HasForeignKey(a => a.RoleGuid);

        // Relation between Booking and Room (1 to Many)
        builder.Entity<Booking>()
               .HasOne(b => b.Room)
               .WithMany(r => r.Bookings)
               .HasForeignKey(b => b.RoomGuid);
    }
}
