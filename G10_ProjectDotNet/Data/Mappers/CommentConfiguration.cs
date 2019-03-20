using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G10_ProjectDotNet.Data.Mappers
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasMany(c => c.Replies)
                .WithOne(r => r.Comment)
                .HasForeignKey("CommentId");
        }
    }
}