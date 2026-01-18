using Askii.backend.Model.Enums;

namespace Askii.backend.DTOs.Question
{
    public class QuestionDTO
    {
        public string QuestionID { get; set; }
        public string SessionID { get; set; }
        public string AskerUID { get; set; }
        public string AskerUserName { get; set; }
        public int Votes { get; set; }
        public QuestionVoteDTO? UserVote { get; set; }
        public string Content { get; set; }
        public string AnswerContent { get; set; } //Answer DTO
        public DateTime CreatedAt { get; set; }
    }
}