using System.ComponentModel.DataAnnotations;

namespace Askii.backend.Model
{
    public class Answer
    {
        [Key]
        public string AnswerID { get; set; }

        [Required]
        public string QuestionID { get; set; }
        public Question Question { get; set; }

        [Required]
        public string ResponderUID { get; set; }
        public User Responder { get; set; }

        [Required]
        public string Content { get; set; }

        public int Votes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
