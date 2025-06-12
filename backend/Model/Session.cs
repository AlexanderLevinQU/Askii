using System.ComponentModel.DataAnnotations;

namespace Askii.backend.Model
{
    public class Session
    {
        public Session()
        {
            //basic constructor
            CreatedAt = DateTime.UtcNow;
        }

        // Primary key
        [Required]
        [Key]
        public string SessionID { get; set; }

        // 1-to-many: One admin leads many sessions
        [Required]
        public string SessionAdminUID { get; set; }
        [Required]
        public User SessionAdmin { get; set; }

        // many-to-many relationships
        public List<User> SessionModerators { get; set; }
        public List<User> SessionAttendees { get; set; }

        // 1-to-many: One session has many questions
        public List<Question> Questions { get; set; }

        [Required]
        public string SessionTopic { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
