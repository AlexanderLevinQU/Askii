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
            {
                return NotFound();
            }

            SessionDTO sessionDTO = new SessionDTO
            {
                SessionID = session.SessionID,
                SessionAdminUID = session.SessionAdminUID,
                SessionTopic = session.SessionTopic,
                SessionAttendeeUIDs = session.SessionAttendees.Select(a => a?.UID).ToList(),
                SessionModeratorUIDs = session.SessionModerators.Select(a => a?.UID).ToList(),
                CreatedAt = session.CreatedAt
            };

            return Ok(sessionDTO);
        }
    }
}