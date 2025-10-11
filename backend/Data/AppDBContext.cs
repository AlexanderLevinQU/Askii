using Microsoft.EntityFrameworkCore;
using Askii.backend.Model; // Adjust based on your namespace

namespace Askii.backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionVote> QuestionVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserSession>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSessions)
                .HasForeignKey(us => us.UID)
                .OnDelete(DeleteBehavior.Cascade); // Optional: tweak based on your logic

            modelBuilder.Entity<UserSession>()
                .HasOne(us => us.Session)
                .WithMany(sp => sp.SessionParticipants)
                .HasForeignKey(us => us.SessionID)
                .OnDelete(DeleteBehavior.Cascade);


            /******* Questions *******/

            //One session to many questions
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Session)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SessionID)
                .OnDelete(DeleteBehavior.Cascade);  // Or Restrict if you prefer

            //Question has one User
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Asker)
                .WithMany()  // if User doesn’t have a navigation collection for questions
                .HasForeignKey(q => q.AskerUID)
                .OnDelete(DeleteBehavior.Restrict);


            /*** UserSessions ***/
            modelBuilder.Entity<UserSession>()
                .HasKey(us => new { us.UID, us.SessionID });

            //So Role is kept as a string instead of an int
            modelBuilder.Entity<UserSession>()
                .Property(us => us.Role)
                .HasConversion<string>();

            /*** Answer ***/

            // One-to-one: Question → Answer
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Answer)
                .WithOne(a => a.Question)
                .HasForeignKey<Answer>(a => a.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);

            // Answer has one User (responder)
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Responder)
                .WithMany()
                .HasForeignKey(a => a.ResponderUID)
                .OnDelete(DeleteBehavior.Restrict);


            /*** Votes ***/
            modelBuilder.Entity<QuestionVote>()
                .HasIndex(v => new { v.UserID, v.QuestionID })
                .IsUnique(); // Only one upvote per user per question

            modelBuilder.Entity<QuestionVote>()
                .HasOne(v => v.Question)
                .WithMany(q => q.Votes)
                .HasForeignKey(v => v.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionVote>()
                .HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserID)
                .OnDelete(DeleteBehavior.Restrict); ;
        }
    }
}
