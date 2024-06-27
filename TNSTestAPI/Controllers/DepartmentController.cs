using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TNSTestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TNSTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the DbContext
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Department
        // Get all departments including their users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _context.departments.ToListAsync();
            return Ok(departments);
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.departments.FirstOrDefaultAsync(d => d.department_id == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // POST: api/Department
        // Create a new department
        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            var next_id = await _context.departments.OrderByDescending(x=> x.department_id).Select(x => x.department_id).FirstOrDefaultAsync() + 1;
            department.department_id = next_id;
            _context.departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.department_id }, department);
        }

        // PUT: api/Department/5
        // Update an existing department by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.department_id)
            {
                return BadRequest("Department ID mismatch");
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Department/5
        // Delete a department by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Check if a department exists by id
        private bool DepartmentExists(int id)
        {
            return _context.departments.Any(e => e.department_id == id);
        }
    }
}
