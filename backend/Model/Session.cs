using Microsoft.AspNetCore.Mvc;

namespace Askii.backend.Model 
{
    public class Session
    {
        public Session () 
        {
            //basic constructor
        }

        public User SessionAdmin { get; set; }
        public List<User> SessionModerators { get; set; }
        public List<User> SessionAttendees { get; set; }
        public List<Question> Questions { get; set; }
        public string SessionTopic { get; set; }
    }
}