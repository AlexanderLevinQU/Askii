using Askii.backend.Data;
using Askii.backend.DTOs.Session;
using Askii.backend.DTOs.User;
using Askii.backend.Model;
using Askii.backend.Model.Enums;
using Askii.backend.Extensions.SessionExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Askii.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {

            User? user = await _context.Users.FindAsync(id); //Can pass other s

            if (user == null)
                return NotFound();

            // Map User to UserDTO TODO: add administered sessions and 
            UserDTO userDTO = new UserDTO();
            userDTO = new UserDTO
            {
                UID = user.UID,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };

            return Ok(userDTO);
        }

        [HttpGet("{UID}/user-with-sessions")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserWithSessions(string UID)
        {
            var user = await _context.Users
                .Include(u => u.UserSessions)
                .ThenInclude(us => us.Session)
                .ThenInclude(s => s.SessionParticipants)
                .ThenInclude(sp => sp.User)
                .FirstOrDefaultAsync(u => u.UID == UID);

            if (user == null)
                return NotFound();

            UserDetailsDTO userDTO = user.ToDetailsDTO();

            return Ok(userDTO);
        }


        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO createUserDto)
        {
            var user = new User
            {
                UID = Guid.NewGuid().ToString(),
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                DateOfBirth = createUserDto.DateOfBirth,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDTO = new UserDTO
            {
                UID = user.UID,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.UID }, userDTO);
        }


        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserDTO userDTO)
        {
            if (id != userDTO.UID)
                return BadRequest();

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            // Update user properties
            user.UserName = userDTO.UserName;
            user.Email = userDTO.Email;
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.DateOfBirth = userDTO.DateOfBirth;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.UID == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Sessions for user specific      

        // GET: api/user/{uid}/sessions?role=Moderator
        [HttpGet("{uid}/sessions")]
        public async Task<ActionResult<List<SessionDTO>>> GetSessionsForUser(
            string uid,
            [FromQuery] UserRole? role = null // optional query param
        )
        {
            var user = await _context.Users
                .Include(u => u.UserSessions)
                    .ThenInclude(us => us.Session)
                        .ThenInclude(s => s.SessionParticipants)
                            .ThenInclude(sp => sp.User)
                .FirstOrDefaultAsync(u => u.UID == uid);

            if (user == null)
                return NotFound("User not found");

            var userSessions = user.UserSessions;

            if (role.HasValue)
            {
                userSessions = userSessions.Where(us => us.Role == role.Value).ToList();
            }

            var sessionDTOs = userSessions
                .Select(us => us.Session.ToDTO())
                .ToList();

            return Ok(sessionDTOs);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Simulate a token
            var token = "fake-jwt-token";

            // Return token + user data (omit sensitive info)
            return Ok(new
            {
                token,
                user = new
                {
                    uid = user.UID,
                    userName = user.UserName,
                    email = user.Email
                }
            });
        }
    }
}