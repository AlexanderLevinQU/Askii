using Askii.backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace backend.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            //One session to many questions
            builder.HasOne(q => q.Session)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SessionID)
                .OnDelete(DeleteBehavior.Cascade);  // Or Restrict if you prefer

            //Question has one User
            builder.HasOne(q => q.Asker)
                .WithMany()  // if User doesn’t have a navigation collection for questions
                .HasForeignKey(q => q.AskerUID)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-one: Question → Answer
            builder.HasOne(q => q.Answer)
                .WithOne(a => a.Question)
                .HasForeignKey<Answer>(a => a.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
