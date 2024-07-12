using Bulut.Class.Student;
using Butut.Class.Student;
using Butut.Context;
using Butut.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Butut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly BulutDbContext _context;

        public StudentsController(BulutDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateStudent student)
        {
            Student entity = new Student();
            entity.OgrNo = student.OgrNo;
            entity.Name = student.Name;
            entity.Surname = student.Surname;
            entity.PlaceOfBirth = student.PlaceOfBirth;
            entity.SectionId = student.SectionId;
            _context.Students.Add(entity);
            _context.SaveChanges();
            return Ok(student);
        }


        [HttpPut]
        public IActionResult Update(UpdateStudent updatedStudent)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.Id == updatedStudent.Id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.OgrNo = updatedStudent.OgrNo;
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Surname = updatedStudent.Surname;
            existingStudent.PlaceOfBirth = updatedStudent.PlaceOfBirth;
            existingStudent.SectionId = updatedStudent.SectionId;

            _context.SaveChanges();
            return Ok(existingStudent);
        }


        [HttpGet]
        public IList<GetStudent> GetAll()
        {
            var students = _context.Students.Include(x => x.Section).ToList();

            var result = students.Select(s => new GetStudent
            {
                Id = s.Id,
                OgrNo = s.OgrNo,
                Name = s.Name,
                Surname = s.Surname,
                PlaceOfBirth = s.PlaceOfBirth,
                SectionId = s.SectionId,
                SectionName = s.Section.SectionName
            }).ToList();

            return result;
        }



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _context.Students.Include(x => x.Section).FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            var result = new GetStudent
            {
                Id = student.Id,
                OgrNo = student.OgrNo,
                Name = student.Name,
                Surname = student.Surname,
                PlaceOfBirth = student.PlaceOfBirth,
                SectionId = student.SectionId,
                SectionName = student.Section.SectionName
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var studentToDelete = _context.Students.FirstOrDefault(s => s.Id == id);
            if (studentToDelete == null)
            {
                return NotFound();
            }

            _context.Students.Remove(studentToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
