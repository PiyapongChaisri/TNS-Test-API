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
            var departments = await _context.Departments
                                            .Include(d => d.Users)
                                            .ToListAsync();
            return Ok(departments);
        }

        // GET: api/Department/5
        // Get a specific department by id including its users
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments
                                           .Include(d => d.Users)
                                           .FirstOrDefaultAsync(d => d.DepartmentId == id);

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
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
        }

        // PUT: api/Department/5
        // Update an existing department by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
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
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Check if a department exists by id
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }
}
