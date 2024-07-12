
using Bulut.Class.Teacher;
using Bulut.Models;
using Butut.Context;
using Butut.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bulut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly BulutDbContext _context;

        public TeacherController(BulutDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateTeacher teacher)
        {
            Teacher entity = new Teacher();
            entity.Name = teacher.Name;
            entity.Surname = teacher.Surname;
            entity.PlaceOfBirth = teacher.PlaceOfBirth;
            entity.SectionId = teacher.SectionId;
            _context.Teacher.Add(entity);
            _context.SaveChanges();
            return Ok(teacher);
        }


        [HttpPut]
        public IActionResult Update(UpdateTeacher updateTeacher)
        {
            var existingTeacher = _context.Teacher.FirstOrDefault(s => s.Id == updateTeacher.Id);
            if (existingTeacher == null)
            {
                return NotFound();
            }

            existingTeacher.Name = updateTeacher.Name;
            existingTeacher.Surname = updateTeacher.Surname;
            existingTeacher.PlaceOfBirth = updateTeacher.PlaceOfBirth;
            existingTeacher.SectionId = updateTeacher.SectionId;

            _context.SaveChanges();
            return Ok(existingTeacher);
        }


        [HttpGet]
        public IList<GetTeacher> GetAll()
        {
            var teachers = _context.Teacher.Include(x => x.Section).ToList();
            var result = teachers.Select(t => new GetTeacher
            {
                Id = t.Id,
                Name = t.Name,
                Surname = t.Surname,
                PlaceOfBirth = t.PlaceOfBirth,
                SectionId = t.SectionId,
                SectionName = t.Section.SectionName
            }).ToList();
            return result;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var teacher = _context.Teacher.Include(x => x.Section).FirstOrDefault(t => t.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            var result = new GetTeacher
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Surname = teacher.Surname,
                PlaceOfBirth = teacher.PlaceOfBirth,
                SectionId = teacher.SectionId,
                SectionName = teacher.Section.SectionName
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var teacherToDelete = _context.Teacher.FirstOrDefault(s => s.Id == id);
            if (teacherToDelete == null)
            {
                return NotFound();
            }

            _context.Teacher.Remove(teacherToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

