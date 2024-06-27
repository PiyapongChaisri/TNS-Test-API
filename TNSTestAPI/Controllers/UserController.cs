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
            var users = await _context.users.ToListAsync();
            return Ok(users);
        }

        // GET: api/User/5
        // Get a specific user by id including their department
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.user_id == id);

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
            var next_id = await _context.users.OrderByDescending(x => x.user_id).Select(x => x.user_id).FirstOrDefaultAsync() + 1;
            user.user_id = next_id;
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.user_id }, user);
        }

        // PUT: api/User/5
        // Update an existing user by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.user_id)
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
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Check if a user exists by id
        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.user_id == id);
        }
    }
}
