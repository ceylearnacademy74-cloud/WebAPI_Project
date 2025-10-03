using backend_EM.Data;
using backend_EM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_EM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public EmployeesController(AppDbContext db) => _db = db;

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<employee>>> Get()
            => await _db.Employees.ToListAsync();

        // GET by id
        [HttpGet("{id}")]
        public async Task<ActionResult<employee>> Get(int id)
        {
            var emp = await _db.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            return emp;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<employee>> Post(employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, employee employee)
        {
            if (id != employee.Id) return BadRequest();
            _db.Entry(employee).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Employees.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _db.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            _db.Employees.Remove(emp);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
