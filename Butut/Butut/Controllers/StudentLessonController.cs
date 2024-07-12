using Bulut.Class.StudentLesson;
using Bulut.Models;
using Butut.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bulut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLessonController : ControllerBase
    {
        private readonly BulutDbContext _context;

        public StudentLessonController(BulutDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateStudentLesson studentLesson)
        {
            StudentLesson entity = new StudentLesson();
            entity.StudentId = studentLesson.StudentId;
            entity.LessonId = studentLesson.LessonId;
            entity.Note = studentLesson.Note;
            _context.StudentLesson.Add(entity);
            _context.SaveChanges();
            return Ok(studentLesson);
        }

        [HttpPut]
        public IActionResult Update(UpdateStudentLesson updateStudentLesson)
        {
            var existingStudentLesson = _context.StudentLesson.FirstOrDefault(sl => sl.StudentId == updateStudentLesson.StudentId && sl.LessonId == updateStudentLesson.LessonId);
            if (existingStudentLesson == null)
            {
                return NotFound();
            }

            existingStudentLesson.Note = updateStudentLesson.Note;

            _context.SaveChanges();
            return Ok(existingStudentLesson);
        }

        [HttpGet]
        public async Task<IList<GetStudentLesson>> GetAll()
        {
            var result = await _context.StudentLesson
                                        .Include(x => x.Student)
                                        .Include(x => x.Lesson)
                                        .Select(x => new GetStudentLesson
                                        {
                                            Id = x.Id,
                                            StudentId = x.StudentId,
                                            StudentName = x.Student.Name + " " + x.Student.Surname,
                                            LessonId = x.LessonId,
                                            LessonName = x.Lesson.Name, 
                                            Note = x.Note
                                        })
                                        .ToListAsync();
            return result;
        }

        [HttpGet("{studentId}/{lessonId}")]
        public IActionResult Get(int studentId, int lessonId)
        {
            var studentLesson = _context.StudentLesson
                                        .Include(x => x.Student)
                                        .Include(x => x.Lesson)
                                        .FirstOrDefault(sl => sl.StudentId == studentId && sl.LessonId == lessonId);
            if (studentLesson == null)
            {
                return NotFound();
            }

            var result = new GetStudentLesson
            {
                Id = studentLesson.Id,
                StudentId = studentLesson.StudentId,
                StudentName = studentLesson.Student.Name + " " + studentLesson.Student.Surname, 
                LessonId = studentLesson.LessonId,
                LessonName = studentLesson.Lesson.Name,
                Note = studentLesson.Note
            };

            return Ok(result);
        }

        [HttpDelete("{studentId}/{lessonId}")]
        public IActionResult Delete(int studentId, int lessonId)
        {
            var studentLessonToDelete = _context.StudentLesson.FirstOrDefault(sl => sl.StudentId == studentId && sl.LessonId == lessonId);
            if (studentLessonToDelete == null)
            {
                return NotFound();
            }

            _context.StudentLesson.Remove(studentLessonToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}