using Bulut.Models;

namespace Butut.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int OgrNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PlaceOfBirth { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public ICollection<StudentLesson> StudentLessons { get; set; }
    }
}
