using Microsoft.AspNetCore.Mvc;
using Askii.backend.Model;
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
            Session? session = await _context.Sessions
                .Include(s => s.SessionAttendees)
                .Include(s => s.SessionModerators)
                .FirstOrDefaultAsync(s => s.SessionID == id);

            if (session == null)
                return NotFound();

            SessionDTO sessionDTO = new SessionDTO
            {
                SessionID = session.SessionID,
                SessionAdminUID = session.SessionAdminUID,
                SessionTopic = session.SessionTopic,
                SessionAttendeeUIDs = session.SessionAttendees?.Select(a => a?.UID).ToList(), //maybe get rid of a? because it is required?
                SessionModeratorUIDs = session.SessionModerators?.Select(a => a?.UID).ToList(),
                CreatedAt = session.CreatedAt
            };

            return Ok(sessionDTO);
        }

        // POST: api/session
        [HttpPost]
        public async Task<ActionResult<SessionDTO>> CreateSession(CreateSessionDTO dto)
        {
            Session session = new Session
            {
                SessionID = Guid.NewGuid().ToString(),
                SessionAdminUID = dto.SessionAdminUID,
                SessionTopic = dto.SessionTopic,
                CreatedAt = DateTime.UtcNow
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            // Return created session
            SessionDTO result = new SessionDTO
            {
                SessionID = session.SessionID,
                SessionAdminUID = session.SessionAdminUID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt
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

        [HttpPost("{sessionId}/add-attendee/{userId}")]
        public async Task<IActionResult> AddAttendeeToSession(string sessionId, string userId)
        {
            var session = await _context.Sessions
                .Include(s => s.SessionAttendees)
                .FirstOrDefaultAsync(s => s.SessionID == sessionId);

            if (session == null)
                return NotFound("Session not found");

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound("User not found");

            if (session.SessionAttendees.Any(u => u.UID == userId))
                return BadRequest("User is already an attendee");

            session.SessionAttendees.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User added as attendee");
        }

        [HttpPost("{sessionId}/add-moderator/{userId}")]
        public async Task<IActionResult> AddModeratorToSession(string sessionId, string userId)
        {
            var session = await _context.Sessions
                .Include(s => s.SessionModerators)
                .FirstOrDefaultAsync(s => s.SessionID == sessionId);

            if (session == null)
                return NotFound("Session not found");

            var user = await _context.Users.FindAsync(userId);
            
            if (user == null)
                return NotFound("User not found");

            if (session.SessionModerators.Any(u => u.UID == userId))
                return BadRequest("User is already an attendee");

            session.SessionModerators.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User added as attendee");
        }

        [HttpDelete("{sessionId}/remove-moderator/{userId}")]
        public async Task<IActionResult> RemoveModeratorFromSession(string sessionId, string userId)
        {
            var session = await _context.Sessions
                .Include(s => s.SessionModerators)
                .FirstOrDefaultAsync(s => s.SessionID == sessionId);

            if (session == null)
                return NotFound("Session not found");

            var user = session.SessionModerators.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return NotFound("User is not an attendee of this session");

            session.SessionModerators.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User removed from attendees");
        }

        [HttpDelete("{sessionId}/remove-attendee/{userId}")]
        public async Task<IActionResult> RemoveAttendeeFromSession(string sessionId, string userId)
        {
            var session = await _context.Sessions
                .Include(s => s.SessionAttendees)
                .FirstOrDefaultAsync(s => s.SessionID == sessionId);

            if (session == null)
                return NotFound("Session not found");

            var user = session.SessionAttendees.FirstOrDefault(u => u.UID == userId);
            if (user == null)
                return NotFound("User is not an attendee of this session");

            session.SessionAttendees.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User removed from attendees");
        }
    }
}