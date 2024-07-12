namespace Bulut.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LessonCode { get; set; }
        public int Credit { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public ICollection<StudentLesson> StudentLessons { get; set; }
        public ICollection<TeacherLesson> TeacherLessons { get; set; }

    }
}
