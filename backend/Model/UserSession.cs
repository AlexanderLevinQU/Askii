using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Askii.backend.Model.Enums;

namespace Askii.backend.Model
{
    public class UserSession
    {
        [Key, Column(Order = 0)]
        [Required]
        public string UID { get; set; }

        [ForeignKey("UID")]
        public User User { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public string SessionID { get; set; }

        [ForeignKey("SessionID")]
        public Session Session { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
