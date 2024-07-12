using Bulut.Models;
using Butut.Models;
using Microsoft.EntityFrameworkCore;

namespace Butut.Context
{
    public class BulutDbContext : DbContext
    {
        public BulutDbContext(DbContextOptions<BulutDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<TeacherLesson> TeacherLesson { get; set; }
        public DbSet<StudentLesson> StudentLesson { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentLesson>(entity =>
            {
                entity.HasKey(sl => sl.Id);
                entity.HasOne(sl => sl.Student)
                      .WithMany(s => s.StudentLessons)
                      .HasForeignKey(sl => sl.StudentId)
                      .OnDelete(DeleteBehavior.NoAction); // Burada değişiklik yapıldı
                entity.HasOne(sl => sl.Lesson)
                      .WithMany(l => l.StudentLessons)
                      .HasForeignKey(sl => sl.LessonId)
                      .OnDelete(DeleteBehavior.NoAction); // Burada değişiklik yapıldı
            });

            modelBuilder.Entity<TeacherLesson>(entity =>
            {
                entity.HasKey(tl => tl.Id);
                entity.HasOne(tl => tl.Teacher)
                      .WithMany(t => t.TeacherLessons)
                      .HasForeignKey(tl => tl.TeacherId)
                      .OnDelete(DeleteBehavior.NoAction); // Burada değişiklik yapıldı
                entity.HasOne(tl => tl.Lesson)
                      .WithMany(l => l.TeacherLessons)
                      .HasForeignKey(tl => tl.LessonId)
                      .OnDelete(DeleteBehavior.NoAction); // Burada değişiklik yapıldı
            });
        }
    }
}
