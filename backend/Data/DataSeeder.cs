using Askii.backend.Model;
using Askii.backend.Model.Enums;

namespace Askii.backend.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var user1 = new User
                {
                    UID = Guid.NewGuid().ToString(),
                    UserName = "jdoe",
                    Email = "jdoe@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    CreatedAt = DateTime.UtcNow
                };

                var user2 = new User
                {
                    UID = Guid.NewGuid().ToString(),
                    UserName = "asmith",
                    Email = "asmith@example.com",
                    FirstName = "Alice",
                    LastName = "Smith",
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.AddRange(user1, user2);
                context.SaveChanges();

                var session = new Session
                {
                    SessionID = Guid.NewGuid().ToString(),
                    SessionTopic = "Intro to C#",
                    CreatedAt = DateTime.UtcNow,
                    SessionAdmin = user1,
                    SessionAdminUID = user1.UID,
                    SessionParticipants = new List<UserSession>
                    {
                        new UserSession
                        {
                            UID = user1.UID,
                            Role = UserRole.Admin
                        },
                        new UserSession
                        {
                            UID = user2.UID,
                            Role = UserRole.Attendee
                        }
                    }
                };

                context.Sessions.Add(session);
                context.SaveChanges();

                var question = new Question
                {
                    QuestionID = Guid.NewGuid().ToString(),
                    Content = "What is a delegate in C#?",
                    AskerUID = user2.UID,
                    SessionID = session.SessionID,
                    CreatedAt = DateTime.UtcNow,
                    Votes = new List<QuestionVote>()
                };

                context.Questions.Add(question);
                context.SaveChanges();
            }
        }
    }
}
