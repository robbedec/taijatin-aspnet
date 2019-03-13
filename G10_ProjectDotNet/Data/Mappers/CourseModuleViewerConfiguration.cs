using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G10_ProjectDotNet.Data.Mappers
{
    public class CourseModuleViewerConfiguration : IEntityTypeConfiguration<CourseModuleViewer>
    {
        public void Configure(EntityTypeBuilder<CourseModuleViewer> builder)
        {
            builder.HasKey(b => new { b.CourseModuleId, b.MemberId });
        }
    }
}