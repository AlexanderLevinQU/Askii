using Askii.backend.Data;
using Askii.backend.DTOs.Question;
using Askii.backend.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace backend.Controllers
{

    public class UpdateQuestionVoteDTO
    {
        public VoteType VoteType { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class QuestionVoteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionVoteController(AppDbContext context)
        {
            _context = context;
        }

        //Move to a question vote controller
        [HttpPost]
        public async Task<ActionResult<QuestionVoteDTO>> CreateQuestionVote(
            string questionId,
            CreateQuestionVoteDTO dto)
        {
            var qVote = new QuestionVote
            {
                VoteID = Guid.NewGuid().ToString(),
                QuestionID = questionId,               // from route
                UserID = dto.UserID,                   // TEMPORARY
                VoteType = dto.VoteType,
                Timestamp = DateTime.UtcNow            // server-owned
            };

            _context.QuestionVotes.Add(qVote);
            await _context.SaveChangesAsync();

            return Ok(new QuestionVoteDTO
            {
                VoteID = qVote.VoteID,
                QuestionID = qVote.QuestionID,
                UserID = qVote.UserID,
                VoteType = qVote.VoteType,
                Timestamp = qVote.Timestamp
            });
        }

        // PUT: api/QuestionVote/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestionVote(string id, [FromBody] UpdateQuestionVoteDTO voteDto) //can change this to a questionVoteDTO
        {
            QuestionVote? questionVote = await _context.QuestionVotes
                .Where(v => v.VoteID == id)
                .SingleOrDefaultAsync();

            if (questionVote == null)
                return NotFound();

            // Update Vote and metadata
            questionVote.VoteType = voteDto.VoteType; // maybe one day track old votes.
            questionVote.Timestamp = DateTime.UtcNow;

            _context.Entry(questionVote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.QuestionVotes.Any(e => e.VoteID == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<IEnumerable<QuestionVoteDTO>>> GetVotesForQuestion(string questionId)
        {
            var votes = await _context.QuestionVotes
                .Where(v => v.QuestionID == questionId)
                .ToListAsync();

            return Ok(votes.Select(v => new QuestionVoteDTO
            {
                VoteID = v.VoteID,
                QuestionID = v.QuestionID,
                UserID = v.UserID,
                VoteType = v.VoteType,
                Timestamp = v.Timestamp
            }));
        }

        [HttpGet("question/{questionId}/summary")]
        public async Task<ActionResult<object>> GetVoteSummary(string questionId)
        {
            var votes = await _context.QuestionVotes
                .Where(v => v.QuestionID == questionId)
                .ToListAsync();

            var upvotes = votes.Count(v => v.VoteType == VoteType.UpVote);
            var downvotes = votes.Count(v => v.VoteType == VoteType.DownVote);
            var noVotes = votes.Count(v => v.VoteType == VoteType.NoVote);

            return Ok(new
            {
                upvotes,
                downvotes,
                noVotes,
                score = upvotes - downvotes
            });
        }

    }
}
