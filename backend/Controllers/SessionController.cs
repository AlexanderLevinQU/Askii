using Microsoft.AspNetCore.Mvc;
using Askii.backend.Model;
using Askii.backend.Model.Enums;
using Askii.backend.DTOs.Session;
using Microsoft.EntityFrameworkCore;
using Askii.backend.Data;

namespace Askii.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SessionController(AppDbContext context)
        {
            _context = context;
        }

        //api/session/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDTO>> GetSession(string id)
        {
            var session = await _context.Sessions
                .Include(s => s.SessionParticipants)
                    .ThenInclude(sp => sp.User)
                .FirstOrDefaultAsync(s => s.SessionID == id);

            if (session == null)
                return NotFound();

            var sessionDTO = new SessionDTO
            {
                SessionID = session.SessionID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt,
                Users = session.SessionParticipants.Select(sp => new SessionUserDTO
                {
                    UID = sp.User.UID,
                    UserName = sp.User.UserName,
                    Role = sp.Role
                }).ToList()
            };

            return Ok(sessionDTO);
        }

        // POST: api/session
        [HttpPost]
        public async Task<ActionResult<SessionDTO>> CreateSession(CreateSessionDTO dto)
        {
            var session = new Session
            {
                SessionID = Guid.NewGuid().ToString(),
                SessionTopic = dto.SessionTopic,
                CreatedAt = DateTime.UtcNow,
                SessionParticipants = new List<UserSession>
                {
                    new UserSession
                    {
                        UID = dto.SessionAdminUID,
                        Role = UserRole.Admin
                    }
                }
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            // Build DTO result
            var result = new SessionDTO
            {
                SessionID = session.SessionID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt,
                Users = session.SessionParticipants.Select(sp => new SessionUserDTO
                {
                    UID = sp.UID,
                    UserName = _context.Users.FirstOrDefault(u => u.UID == sp.UID)?.UserName ?? "",
                    Role = sp.Role
                }).ToList()
            };

            return CreatedAtAction(nameof(GetSession), new { id = session.SessionID }, result);
        }


        // DELETE: api/session/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(string id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
                return NotFound();

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{sessionId}/add-user/{userId}")]
        public async Task<IActionResult> AddUserToSession(string sessionId, string userId, [FromQuery] UserRole role)
        {
            var session = await _context.Sessions
                .Include(s => s.SessionParticipants)
                .FirstOrDefaultAsync(s => s.SessionID == sessionId);

            if (session == null)
                return NotFound("Session not found");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found");

            if (session.SessionParticipants.Any(sp => sp.UID == userId))
                return BadRequest("User is already a participant in this session");

            session.SessionParticipants.Add(new UserSession
            {
                UID = userId,
                Role = role
            });

            await _context.SaveChangesAsync();

            return Ok($"User added as {role}");
        }

        [HttpDelete("{sessionId}/remove-user/{userId}")]
        public async Task<IActionResult> RemoveUserFromSession(string sessionId, string userId, [FromQuery] UserRole role)
        {
            var session = await _context.Sessions
                .Include(s => s.SessionParticipants)
                .FirstOrDefaultAsync(s => s.SessionID == sessionId);

            if (session == null)
                return NotFound("Session not found");

            var participant = session.SessionParticipants
                .FirstOrDefault(sp => sp.UID == userId && sp.Role == role);

            if (participant == null)
                return NotFound($"User with role '{role}' is not part of this session.");

            if (participant.Role == UserRole.Admin)
                return Forbid("Removing Admins is not allowed through this endpoint.");

            session.SessionParticipants.Remove(participant);
            await _context.SaveChangesAsync();

            return Ok($"User removed from session as {role}.");
        }

    }
}