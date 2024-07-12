using Butut.Models;

namespace Bulut.Models
{
    public class StudentLesson
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public int Note { get; set; }

        // Navigation properties
        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
    }
}
