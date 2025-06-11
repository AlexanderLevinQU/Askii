using Microsoft.AspNetCore.Mvc;

namespace Askii.backend.Model 
{
    public class User
    {
        public User ()
        {
            //basic constructor
        }

        public string UID {get; private set; }
        public string UserName { get; set; }
        public string Email { get; set; }   

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public List<Session> Sessions { get; set; }

        // Optional metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}