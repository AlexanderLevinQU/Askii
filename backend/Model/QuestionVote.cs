using System.ComponentModel.DataAnnotations;
using Askii.backend.Model;
using Askii.backend.Model.Enums;

public class QuestionVote
{
    [Required]
    [Key]
    public string VoteID { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string? QuestionID { get; set; }
    public Question? Question { get; set; }

    [Required]
    public string? UserID { get; set; }
    public User? User { get; set; }

    [Required]
    public VoteType VoteType { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
