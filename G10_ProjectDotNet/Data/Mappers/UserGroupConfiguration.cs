using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data.Mappers
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(b => new { b.MemberId, b.GroupId });

            builder.HasOne(b => b.Member).WithMany(b => b.UserGroups).HasForeignKey(b => b.MemberId);
            builder.HasOne(b => b.Group).WithMany(b => b.UserGroups).HasForeignKey(b => b.GroupId);
        }
    }
}
