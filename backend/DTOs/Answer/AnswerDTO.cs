using Askii.backend.DTOs.Question;
using Askii.backend.DTOs.User;


public class AnswerDTO
{
    public string AnswerID { get; set; }

    public string QuestionID { get; set; }
    public QuestionDTO Question { get; set; }

    public string ResponderUID { get; set; }
    public UserDTO Responder { get; set; }

    public string Content { get; set; }

    public int Votes { get; set; }

    public DateTime CreatedAt { get; set; }
}