using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G10_ProjectDotNet.Data.Mappers
{
    public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
    {
        public void Configure(EntityTypeBuilder<CourseModule> builder)
        {
            builder.HasKey(cm => cm.CourseModuleId);

            builder.HasMany(cm => cm.Comments)
                .WithOne(c => c.CourseModule)
                .HasForeignKey("CourseModuleId");
        }
    }
}