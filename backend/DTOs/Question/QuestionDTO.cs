namespace Askii.backend.DTOs.Question
{
    public class QuestionDTO
    {
        public string QuestionID { get; set; }
        public string SessionID { get; set; }
        public string AskerUID { get; set; }
        public string AskerUserName { get; set; }
        public int Votes { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}