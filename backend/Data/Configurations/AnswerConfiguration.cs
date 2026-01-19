using Askii.backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace backend.Data.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {

        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            // Answer has one User (responder)
            builder.HasOne(a => a.Responder)
                .WithMany()
                .HasForeignKey(a => a.ResponderUID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
