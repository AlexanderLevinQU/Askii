using Askii.backend.Model;

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
                    SessionAdminUID = user1.UID,
                    SessionTopic = "Intro to C#",
                    CreatedAt = DateTime.UtcNow,
                    SessionAttendees = new List<User> { user1, user2}
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
                    Votes = 3
                };

                context.Questions.Add(question);
                context.SaveChanges();
            }
        }
    }
}
