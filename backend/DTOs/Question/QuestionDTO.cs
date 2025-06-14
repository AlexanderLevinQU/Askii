namespace Askii.backend.DTOs.Question
{
    public class QuestionDTO
    {
        public string QuestionId { get; set; }
        public string SessionID { get; set; }
        public string AskerUID { get; set; }
        public int Votes { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}