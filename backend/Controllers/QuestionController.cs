using Microsoft.AspNetCore.Mvc;
using Askii.backend.Model;
using Microsoft.EntityFrameworkCore;
using Askii.backend.Data;
using Askii.backend.DTOs.Question;

namespace Askii.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionController(AppDbContext context)
        {
            _context = context;
        }

        //api/question/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> GetQuestion(string id)
        {
            Question? question = await _context.Questions
                .Include(q => q.SessionID)
                .Include(q => q.AskerUID)
                .FirstOrDefaultAsync(q => q.QuestionID == id);

            if (question == null)
                return NotFound();

            QuestionDTO questionDTO = new QuestionDTO()
            {
                QuestionID = question.QuestionID,
                SessionID = question.SessionID,
                AskerUID = question.AskerUID,
                Votes = question.Votes,
                Content = question.Content,
                CreatedAt = question.CreatedAt,
            };

            return Ok(questionDTO);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> CreateQuestion(CreateQuestionDTO questionDTO)
        {
            Question question = new Question()
            {
                QuestionID = Guid.NewGuid().ToString(),
                SessionID = questionDTO.SessionID,
                AskerUID = questionDTO.AskerUID,
                Votes = 0,
                Content = questionDTO.Content,
                CreatedAt = DateTime.UtcNow
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            // Return created question
            QuestionDTO result = new QuestionDTO
            {
                QuestionID = question.QuestionID,
                AskerUID = question.AskerUID,
                Content = question.Content,
                Votes = question.Votes,
                CreatedAt = question.CreatedAt,
                SessionID = question.SessionID
            };

            return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionID }, result);
        }

        // TODO: Maybe move to sessions
        // GET: api/question/session/{sessionId}
        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<List<QuestionDTO>>> GetQuestionsForSession(string sessionId) 
        {
            var questions = await _context.Questions
                .Where(q => q.SessionID == sessionId)
                .OrderByDescending(q => q.Votes)
                .ToListAsync();

            var questionDTOs = questions.Select(q => new QuestionDTO
            {
                QuestionID = q.QuestionID,
                AskerUID = q.AskerUID,
                Content = q.Content,
                Votes = q.Votes,
                CreatedAt = q.CreatedAt,
                SessionID = q.SessionID
            }).ToList();

            return Ok(questionDTOs);
        }

        // DELETE: api/question/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            Question? question = await _context.Questions.FindAsync(id);

            if (question == null)
                return NotFound();

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}