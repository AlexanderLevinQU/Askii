namespace Askii.backend.DTOs.Question
{
    public class CreateAnswerDTO
    {
        public string SessionID { get; set; }
        public string AskerUID { get; set; }
        public string QuestionID { get; set; }
        public string ResponderUID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}