namespace Bulut.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PlaceOfBirth { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public ICollection<TeacherLesson> TeacherLessons { get; set; }
    }
}
