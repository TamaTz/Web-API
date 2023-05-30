using API.Models;
using API.Utility;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(new Role
            {
                Guid = Guid.Parse("40ac62a2-392e-4eb2-2f69-08db60bf1e9a"),
                Name = nameof(RoleLevel.User),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
                new Role
            {
                Guid = Guid.Parse("ec576a69-d7ae-42a7-2f6a-08db60bf1e9a"),
                Name = nameof(RoleLevel.Manager),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
                },
                new Role
            {
                Guid = Guid.Parse("3fb23dae-feed-41fe-2f6b-08db60bf1e9a"),
                Name = nameof(RoleLevel.Admin),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
                });

            builder.Entity<Employee>().HasIndex(e => new
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            //Relasi dari education ke university (1 to Many)
            builder.Entity<Education>()
                   .HasOne(u => u.University)
                   .WithMany(e => e.Educations)
                   .HasForeignKey(e => e.UniversityGuid);

            //Relasi dari Education ke Employee (1 to 1)
            builder.Entity<Education>()
                   .HasOne(e => e.Employee)
                   .WithOne(e => e.Education)
                   .HasForeignKey<Education>(e => e.Guid);

            //Relasi dari Akun ke Employee (1 to 1)
            builder.Entity<Account>()
                   .HasOne(e => e.Employee)
                   .WithOne(a => a.Account)
                   .HasForeignKey<Account>(a => a.Guid);

            //Relasi dari Akun Roles ke Akun (1 to Many)
            builder.Entity<AccountRole>()
                   .HasOne(a => a.Account)
                   .WithMany(a => a.AccountRole)
                   .HasForeignKey(a => a.AccountGuid);

            //Relasi dari Akun Role ke Role (1 to Many)
            builder.Entity<AccountRole>()
                   .HasOne(r => r.Role)
                   .WithMany(a => a.AccountRole)
                   .HasForeignKey(a => a.RoleGuid);

            //Relasi Booking ke Employe (1 to Many)
            builder.Entity<Booking>()
                   .HasOne(e => e.Employee)
                   .WithMany(b => b.Bookings)
                   .HasForeignKey(b => b.EmployeeGuid);

            //Relalsi Booking ke Room (1 to Many)
            builder.Entity<Booking>()
                   .HasOne(r => r.Room)
                   .WithMany(b => b.Bookings)
                   .HasForeignKey(b => b.RoomGuid);

        }
    }
}
