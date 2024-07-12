using Bulut.Class.Section;
using Bulut.Models;
using Butut.Context;
using Microsoft.AspNetCore.Mvc;

namespace Bulut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly BulutDbContext _context;

        public SectionController(BulutDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateSection section)
        {
            Section entity = new Section
            {
                SectionName = section.SectionName,
                Description = section.Description,
                Email = section.Email
            };
            _context.Section.Add(entity);
            _context.SaveChanges();
            return Ok(section);
        }

        [HttpPut]
        public IActionResult Update(UpdateSection updateSection)
        {
            var existingSection = _context.Section.FirstOrDefault(s => s.Id == updateSection.Id);
            if (existingSection == null)
            {
                return NotFound();
            }

            existingSection.SectionName = updateSection.SectionName;
            existingSection.Description = updateSection.Description;
            existingSection.Email = updateSection.Email;

            _context.SaveChanges();
            return Ok(existingSection);
        }

        [HttpGet]
        public IList<Section> GetAll()
        {
            return _context.Section.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var section = _context.Section.FirstOrDefault(s => s.Id == id);
            if (section == null)
            {
                return NotFound();
            }
            return Ok(section);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sectionToDelete = _context.Section.FirstOrDefault(s => s.Id == id);
            if (sectionToDelete == null)
            {
                return NotFound();
            }

            _context.Section.Remove(sectionToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
