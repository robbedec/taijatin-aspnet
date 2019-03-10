using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data.Mappers
{
    public class Formula_FormulaDayConfiguration : IEntityTypeConfiguration<FormulaFormulaDay>
    {
        public void Configure(EntityTypeBuilder<FormulaFormulaDay> builder)
        {
            builder.HasKey(b => new { b.FormulaId, b.FormulaDayId });
        }
    }
}
