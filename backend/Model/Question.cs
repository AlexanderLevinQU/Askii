using System.ComponentModel.DataAnnotations;

namespace Askii.backend.Model
{
    public class Question
    {
        public Question()
        {
            //basic constructor
            CreatedAt = DateTime.UtcNow;
            Votes = new List<QuestionVote>();
            Content = string.Empty;
        }

        [Required]
        [Key]
        // Primary key
        public string QuestionID { get; set; }

        // Foreign key for Session
        [Required]
        public string SessionID { get; set; }
        [Required]
        public Session Session { get; set; }

        // Foreign key for User (asker)
        [Required]
        public string AskerUID { get; set; }
        [Required]
        public User Asker { get; set; }

        public ICollection<QuestionVote> Votes { get; set; } = new List<QuestionVote>();

        // Better naming for the text of the question
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public Answer Answer { get; set; } //Answer to question
    }
}
