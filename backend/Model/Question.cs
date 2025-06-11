using Microsoft.AspNetCore.Mvc;

namespace Askii.backend.Model 
{
    public class Question
    {
        
        public Question()
        {
            //basic constructor
        }

        public Session Session { get; set; }
        public User QuestionAsker { get; set; } //rename
        public int Votes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}