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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-many for Admins
            modelBuilder.Entity<Session>()
                .HasOne(s => s.SessionAdmin)
                .WithMany(u => u.AdministeredSessions) // <-- navigation property here
                .HasForeignKey(s => s.SessionAdminUID)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many for attendees
            modelBuilder.Entity<Session>()
                .HasMany(s => s.SessionAttendees)
                .WithMany(u => u.AttendedSessions)
                .UsingEntity(j => j.ToTable("SessionAttendees"));

            // Many-to-many for moderators
            modelBuilder.Entity<Session>()
                .HasMany(s => s.SessionModerators)
                .WithMany(u => u.ModeratedSessions)
                .UsingEntity(j => j.ToTable("SessionModerators")); ;

            //Questions

            //One session to many questions
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Session)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SessionID)
                .OnDelete(DeleteBehavior.Cascade);  // Or Restrict if you prefer

            //Question has on User
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Asker)
                .WithMany()  // if User doesn’t have a navigation collection for questions
                .HasForeignKey(q => q.AskerUID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
