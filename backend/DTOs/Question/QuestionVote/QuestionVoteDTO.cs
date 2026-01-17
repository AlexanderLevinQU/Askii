
using Askii.backend.Model.Enums;

namespace Askii.backend.DTOs.Question
{
    public class QuestionVoteDTO
    {
        public string VoteID { get; set; } = default!;
        public string QuestionID { get; set; } = default!;
        public string UserID { get; set; } = default!;
        public VoteType VoteType{ get; set; } = default!;
        public DateTime Timestamp { get; set; }
    }
}