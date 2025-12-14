using Microsoft.EntityFrameworkCore;

namespace firstpr.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.CoursesTaught)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseStudents",
                    r => r.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
                    l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                    je =>
                    {
                        je.HasKey("CourseId", "StudentId");
                        je.ToTable("CourseStudents");
                    });
        }
    }
}
