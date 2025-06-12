using System.ComponentModel.DataAnnotations;

namespace Askii.backend.Model
{
    public class Question
    {
        public Question()
        {
            //basic constructor
            CreatedAt = DateTime.UtcNow;
            Votes = 0;
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

        public int Votes { get; set; }

        // Better naming for the text of the question
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
