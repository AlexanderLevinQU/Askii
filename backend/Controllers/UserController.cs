using Askii.backend.Data;
using Askii.backend.DTOs.Session;
using Askii.backend.DTOs.User;
using Askii.backend.Model;
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

        [HttpGet("{UID}/user-wth-sessions")]
        public async Task<ActionResult<UserDetailsDTO>> GetUserWithSessions(string UID)
        {
            var user = await _context.Users
                .Include(u => u.AdministeredSessions)
                .Include(u => u.ModeratedSessions)
                .Include(u => u.AttendedSessions)
                .FirstOrDefaultAsync(u => u.UID == UID);

            if (user == null)
                return NotFound();

            var userDTO = new UserDetailsDTO
            {
                UID = user.UID,
                UserName = user.UserName,
                Email = user.Email,
                AdministeredSessions = user.AdministeredSessions.Select(s => s.ToDTO()).ToList(),
                ModeratedSessions = user.ModeratedSessions.Select(s => s.ToDTO()).ToList(),
                AttendedSessions = user.AttendedSessions.Select(s => s.ToDTO()).ToList()
            };

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

        // GET: api/user/{uid}/sessions
        [HttpGet("{UID}/sessions")]
        public async Task<ActionResult<List<SessionDTO>>> GetSessionsForUser(string UID)
        {
            var sessions = await _context.Sessions
                .Include(s => s.SessionAttendees)
                .Include(s => s.SessionModerators)
                .Where(s =>
                    s.SessionAdminUID == UID ||
                    s.SessionModerators.Any(m => m.UID == UID) ||
                    s.SessionAttendees.Any(a => a.UID == UID))
                .ToListAsync();

            var sessionDTOs = sessions.Select(session => new SessionDTO
            {
                SessionID = session.SessionID,
                SessionAdminUID = session.SessionAdminUID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt,
                SessionAttendeeUIDs = session.SessionAttendees?.Select(a => a?.UID).ToList(),
                SessionModeratorUIDs = session.SessionModerators?.Select(m => m?.UID).ToList()
            }).ToList();

            return Ok(sessionDTOs);
        }

        // GET: api/user/{uid}/moderated-sessions
        [HttpGet("{UID}/moderated-sessions")]
        public async Task<ActionResult<List<SessionDTO>>> GetModeratedSessionsForUser(string UID)
        {
            var sessions = await _context.Sessions
                .Include(s => s.SessionModerators)
                .Where(s => s.SessionModerators.Any(m => m.UID == UID))
                .ToListAsync();

            var sessionDTOs = sessions.Select(session => new SessionDTO
            {
                SessionID = session.SessionID,
                SessionAdminUID = session.SessionAdminUID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt,
                SessionAttendeeUIDs = session.SessionAttendees?.Select(a => a?.UID).ToList(),
                SessionModeratorUIDs = session.SessionModerators?.Select(m => m?.UID).ToList()
            }).ToList();

            return Ok(sessionDTOs);
        }

        // GET: api/user/{uid}/attendee-sessions
        [HttpGet("{UID}/attendee-sessions")]
        public async Task<ActionResult<List<SessionDTO>>> GetAttendeeSessionsForUser(string UID)
        {
            var sessions = await _context.Sessions
                .Include(s => s.SessionAttendees)
                .Where(s => s.SessionAttendees.Any(m => m.UID == UID))
                .ToListAsync();

            var sessionDTOs = sessions.Select(session => new SessionDTO
            {
                SessionID = session.SessionID,
                SessionAdminUID = session.SessionAdminUID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt,
                SessionAttendeeUIDs = session.SessionAttendees?.Select(a => a?.UID).ToList(),
                SessionModeratorUIDs = session.SessionModerators?.Select(m => m?.UID).ToList()
            }).ToList();

            return Ok(sessionDTOs);
        }

        // GET: api/user/{uid}/admin-sessions
        [HttpGet("{UID}/admin-sessions")]
        public async Task<ActionResult<List<SessionDTO>>> GetAdminSessionsForUser(string UID)
        {
            var sessions = await _context.Sessions
                .Where(s => s.SessionAdmin.UID == UID)
                .ToListAsync();

            var sessionDTOs = sessions.Select(session => new SessionDTO
            {
                SessionID = session.SessionID,
                SessionAdminUID = session.SessionAdminUID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt,
                SessionAttendeeUIDs = session.SessionAttendees?.Select(a => a?.UID).ToList(),
                SessionModeratorUIDs = session.SessionModerators?.Select(m => m?.UID).ToList()
            }).ToList();

            return Ok(sessionDTOs);
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            /** no password for now
            if (user == null || user.Password != request.Password)
            {
                return Unauthorized("Invalid credentials");
            }
            **/

            // Simulate a token
            var token = "fake-jwt-token";

            return Ok(new { token });
        }
        
    }
}