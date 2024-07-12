using Bulut.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Butut.Context;
using Bulut.Class.TeacherLesson;
using Bulut.Class.StudentLesson;
using Microsoft.EntityFrameworkCore;

namespace Bulut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherLessonController : ControllerBase
    {
        private readonly BulutDbContext _context;

        public TeacherLessonController(BulutDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateTeacherLesson teacherLesson)
        {
            TeacherLesson entity = new TeacherLesson();
            entity.TeacherId = teacherLesson.TeacherId;
            entity.LessonId = teacherLesson.LessonId;
            _context.TeacherLesson.Add(entity);
            _context.SaveChanges();
            return Ok(teacherLesson);
        }

        [HttpPut]
        public IActionResult Update(UpdateTeacherLesson updateTeacherLesson)
        {
            var existingTeacherLesson = _context.TeacherLesson.FirstOrDefault(tl => tl.TeacherId == updateTeacherLesson.TeacherId && tl.LessonId == updateTeacherLesson.LessonId);
            if (existingTeacherLesson == null)
            {
                return NotFound();
            }

            existingTeacherLesson.TeacherId = updateTeacherLesson.TeacherId;
            existingTeacherLesson.LessonId = updateTeacherLesson.LessonId;

            _context.SaveChanges();
            return Ok(existingTeacherLesson);
        }

        [HttpGet]
        public IList<GetTeacherLesson> GetAll()
        {
            var teacherLessons = _context.TeacherLesson
                                        .Include(x => x.Teacher)
                                        .Include(x => x.Lesson)
                                        .ToList();

            var result = teacherLessons.Select(teacherLesson => new GetTeacherLesson
            {
                TeacherId = teacherLesson.TeacherId,
                TeacherName = teacherLesson.Teacher.Name + " " + teacherLesson.Teacher.Surname, 
                LessonId = teacherLesson.LessonId,
                LessonName = teacherLesson.Lesson.Name 
            }).ToList();

            return result;
        }

        [HttpGet("{teacherId}/{lessonId}")]
        public IActionResult Get(int teacherId, int lessonId)
        {
            var teacherLesson = _context.TeacherLesson
                                    .Include(x => x.Teacher)
                                    .Include(x => x.Lesson)
                                    .FirstOrDefault(tl => tl.TeacherId == teacherId && tl.LessonId == lessonId);

            if (teacherLesson == null)
            {
                return NotFound();
            }

            var result = new GetTeacherLesson
            {
                TeacherId = teacherLesson.TeacherId,
                TeacherName = teacherLesson.Teacher.Name +" "+ teacherLesson.Teacher.Surname, 
                LessonId = teacherLesson.LessonId,
                LessonName = teacherLesson.Lesson.Name 
            };

            return Ok(result);
        }

        [HttpDelete("{teacherId}/{lessonId}")]
        public IActionResult Delete(int teacherId, int lessonId)
        {
            var teacherLessonToDelete = _context.TeacherLesson.FirstOrDefault(tl => tl.TeacherId == teacherId && tl.LessonId == lessonId);
            if (teacherLessonToDelete == null)
            {
                return NotFound();
            }

            _context.TeacherLesson.Remove(teacherLessonToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
