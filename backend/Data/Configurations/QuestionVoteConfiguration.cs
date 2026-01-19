using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace backend.Data.Configurations
{
    public class QuestionVoteConfiguration : IEntityTypeConfiguration<QuestionVote>
    {
        public void Configure(EntityTypeBuilder<QuestionVote> builder)
        {
            /*** Votes ***/
            builder.HasIndex(v => new { v.UserID, v.QuestionID })
                .IsUnique(); // Only one upvote per user per question

            builder.HasOne(v => v.Question)
                .WithMany(q => q.Votes)
                .HasForeignKey(v => v.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
