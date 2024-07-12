using Bulut.Class.Lesson;
using Bulut.Models;
using Butut.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bulut.Controllers
{
    namespace Bulut.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class LessonController : ControllerBase
        {
            private readonly BulutDbContext _context;

            public LessonController(BulutDbContext context)
            {
                _context = context;
            }

            [HttpPost]
            public IActionResult Create(CreateLesson lesson)
            {
                Lesson entity = new Lesson
                {
                    Name = lesson.Name,
                    LessonCode = lesson.LessonCode,
                    Credit = lesson.Credit,
                    SectionId = lesson.SectionId
                };
                _context.Lesson.Add(entity);
                _context.SaveChanges();
                return Ok(lesson);
            }

            [HttpPut]
            public IActionResult Update(UpdateLesson updateLesson)
            {
                var existingLesson = _context.Lesson.FirstOrDefault(l => l.Id == updateLesson.Id);
                if (existingLesson == null)
                {
                    return NotFound();
                }

                existingLesson.Name = updateLesson.Name;
                existingLesson.LessonCode = updateLesson.LessonCode;
                existingLesson.Credit = updateLesson.Credit;
                existingLesson.SectionId = updateLesson.SectionId;

                _context.SaveChanges();
                return Ok(existingLesson);
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                var lessons = _context.Lesson
                                .Include(x => x.Section)
                                .ToList();

                var result = lessons.Select(lesson => new GetLesson
                {
                    Id = lesson.Id,
                    Name = lesson.Name,
                    LessonCode = lesson.LessonCode,
                    Credit = lesson.Credit,
                    SectionName = lesson.Section.SectionName,
                    SectionId = lesson.Section.Id
                }).ToList();

                return Ok(result);
            }

            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                var lesson = _context.Lesson.Include(x => x.Section).FirstOrDefault(l => l.Id == id);
                if (lesson == null)
                {
                    return NotFound();
                }

                var result = new GetLesson
                {
                    Id=lesson.Id,
                    Name = lesson.Name,
                    LessonCode = lesson.LessonCode,
                    Credit = lesson.Credit,
                    SectionName = lesson.Section.SectionName,
                    SectionId = lesson.Section.Id
                };

                return Ok(result);
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var lessonToDelete = _context.Lesson.FirstOrDefault(l => l.Id == id);
                if (lessonToDelete == null)
                {
                    return NotFound();
                }

                _context.Lesson.Remove(lessonToDelete);
                _context.SaveChanges();
                return NoContent();
            }
        }
    }
}
