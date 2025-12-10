using Microsoft.EntityFrameworkCore;

namespace firstpr.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext() { }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=School_DB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // رابطه یک به چند: معلم → درس‌ها
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.CoursesTaught)           // اگر در Teacher تعریف کردی
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            // رابطه چند به چند: درس‌ها و دانشجویان
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