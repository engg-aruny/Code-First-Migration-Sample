using Microsoft.EntityFrameworkCore;

namespace Code_First_Migration_Sample.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TeacherEntity> Teachers { get; set; }

        public DbSet<StudentEntity> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure Teacher
            modelBuilder.Entity<TeacherEntity>().
                 ToTable("Teachers");

            modelBuilder.Entity<TeacherEntity>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<TeacherEntity>().Property(t => t.Email)
                 .IsRequired()
                 .HasMaxLength(50);

            modelBuilder.Entity<TeacherEntity>().Property(t => t.PhoneNumber)
               .IsRequired()
               .HasMaxLength(10);

            modelBuilder.Entity<TeacherEntity>().Property(t => t.Gender)
               .IsRequired()
               .HasMaxLength(1);

            // Rename the Teachers table to Educators
            modelBuilder.Entity<TeacherEntity>()
                .ToTable("Educators");

            // Add an index to the Educators table on the Name column
            modelBuilder.Entity<TeacherEntity>()
                .HasIndex(x => x.Email)
                .HasDatabaseName("IX_Educators_Email")
                .HasFilter(null)
                .IsUnique();

            //Configure Student
            modelBuilder.Entity<StudentEntity>().
                 ToTable("Students");

            modelBuilder.Entity<StudentEntity>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<StudentEntity>()
               .Property(x => x.DateOfBirth)
                .IsRequired();

            modelBuilder.Entity<StudentEntity>()
               .Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(50);

            modelBuilder.Entity<TeacherEntity>().HasData(
            new TeacherEntity
            {
                ID = 1,
                FirstName = "Ramesh",
                LastName = "Kumar",
                Email = "testramesh@gmail.com",
                PhoneNumber = "1234567890",
                Gender = "M"
            },
            new TeacherEntity
            {
                ID = 2,
                FirstName = "Amit ",
                LastName = "Sharma",
                Email = "testamitsharma@gmail.com",
                PhoneNumber = "1234517890",
                Gender = "M"
            });

            modelBuilder.Entity<StudentEntity>().HasData(
            new StudentEntity
            {
                ID = 1,
                FirstName = "Mohit",
                LastName = "Yadav",
                Email = "testmohit@gmail.com",
                DateOfBirth = DateTime.Now,
            },
            new StudentEntity
            {
                ID = 2,
                FirstName = "Ankit",
                LastName = "Sharma",
                Email = "testankitsharma@gmail.com",
                DateOfBirth = DateTime.Now,
            });
        }
    }
}
