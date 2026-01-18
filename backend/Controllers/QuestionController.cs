using Microsoft.AspNetCore.Mvc;
using Askii.backend.Model;
using Microsoft.EntityFrameworkCore;
using Askii.backend.Data;
using Askii.backend.DTOs.Question;
using Askii.backend.Model.Enums;

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
                .Include(q => q.Session)
                .Include(q => q.Asker)
                .Include(q=>q.Votes)
                .FirstOrDefaultAsync(q => q.QuestionID == id);

            if (question == null)
                return NotFound();

            int votes = question.Votes.Count(x => x.VoteType == VoteType.UpVote) - question.Votes.Count(x => x.VoteType == VoteType.DownVote);

            QuestionDTO questionDTO = new QuestionDTO()
            {
                QuestionID = question.QuestionID,
                SessionID = question.SessionID,
                AskerUID = question.AskerUID,
                Votes = votes,
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
                Votes = new List<QuestionVote>(),
                Content = questionDTO.Content,
                CreatedAt = DateTime.UtcNow
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            User asker = await _context.Users.FindAsync(question.AskerUID);

            // Return created question
            QuestionDTO result = new QuestionDTO
            {
                QuestionID = question.QuestionID,
                AskerUID = question.AskerUID,
                AskerUserName = asker.UserName ?? "Unknown",
                Content = question.Content,
                Votes = 0,
                CreatedAt = question.CreatedAt,
                SessionID = question.SessionID
            };

            return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionID }, result);
        }

        // TODO: Maybe move to sessions
        // GET: api/question/session/{sessionId}
        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<List<QuestionDTO>>> GetQuestionsForSession(string sessionId, string userId) 
        {
            var questions = await _context.Questions
                .Include(q => q.Asker)
                .Include(q => q.Votes)
                .Where(q => q.SessionID == sessionId)
                .ToListAsync();

            var questionDTOs = questions
                .Select(q =>
                {
                    var upvotes = q.Votes.Count(v => v.VoteType == VoteType.UpVote);
                    var downvotes = q.Votes.Count(v => v.VoteType == VoteType.DownVote);
                    var noVotes = q.Votes.Count(v => v.VoteType == VoteType.NoVote);
                    var score = upvotes - downvotes;
                    var questionVote = q.Votes.FirstOrDefault(v => v.UserID == userId && q.QuestionID == v.QuestionID);

                    QuestionVoteDTO? userVote = null;
                    if (questionVote != null)
                    {
                        userVote = new QuestionVoteDTO
                        {
                            VoteID = questionVote.VoteID,
                            QuestionID = questionVote.QuestionID,
                            UserID = questionVote.UserID,
                            VoteType = questionVote.VoteType,
                            Timestamp = questionVote.Timestamp
                        };
                    }

                   return new QuestionDTO
                    {
                        QuestionID = q.QuestionID,
                        AskerUID = q.AskerUID,
                        AskerUserName = q.Asker.UserName,
                        Content = q.Content,
                        Votes = score,
                        UserVote = userVote,
                        CreatedAt = q.CreatedAt,
                        SessionID = q.SessionID
                    };
                })
                .OrderByDescending(q => q.Votes) // sort AFTER computing score
                .ToList();

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