namespace Askii.backend.DTOs.Question
{
    public class CreateQuestionDTO
    {
        public string SessionID { get; set; }
        public string AskerUID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}