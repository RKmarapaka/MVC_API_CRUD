using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_CRUD.Data;
using API_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly Context _context;

        public StudentController(Context context)
        {
            _context = context;
        }


        // Get All Students
        [HttpGet]
        public List<Student> Get()
        {
            return _context.students.ToList();
        }

        
        [HttpGet("{Id}")]
        public Student GetStudent(int Id)
        {
            var student = _context.students.Where(s => s.Id == Id).SingleOrDefault();
            return student;
        }

        [HttpPost]
        public IActionResult PostStudent([FromBody]Student student)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Not a Valid Model");
            }
            _context.students.Add(student);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student =await _context.students.FindAsync(id);

            if(student == null)
            {
                return NotFound();
            }
            _context.students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
       [HttpPut("{Id}")]
        public async Task<IActionResult> PutStudent(int id)
        {
            var student = await _context.students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            _context.students.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}