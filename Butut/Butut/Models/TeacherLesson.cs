namespace Bulut.Models
{
    public class TeacherLesson
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int LessonId { get; set; }
        public Teacher Teacher { get; set; }
        public Lesson Lesson { get; set; }
    }
}
