using Askii.backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.HasKey(us => new { us.UID, us.SessionID });

        builder.HasOne(us => us.User)
               .WithMany(u => u.UserSessions)
               .HasForeignKey(us => us.UID)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(us => us.Session)
               .WithMany(s => s.SessionParticipants)
               .HasForeignKey(us => us.SessionID)
               .OnDelete(DeleteBehavior.Cascade);


        //So Role is kept as a string instead of an int
        builder.Property(us => us.Role)
               .HasConversion<string>();
    }
}