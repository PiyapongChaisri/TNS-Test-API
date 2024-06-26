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
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the DbContext
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        // Get all users including their department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.Department).ToListAsync();
            return Ok(users);
        }

        // GET: api/User/5
        // Get a specific user by id including their department
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.Department)
                                           .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/User
        // Create a new user
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // PUT: api/User/5
        // Update an existing user by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("User ID mismatch");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/User/5
        // Delete a user by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Check if a user exists by id
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
