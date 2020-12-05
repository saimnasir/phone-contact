using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.Repositories.SchemaDefinitions
{
    public class ReportEntitySchemaConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Report".ToUpper(), ReportingContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.UIID);
            builder.Property(p => p.CreateDate).IsRequired();
            builder.Property(p => p.UpdateDate);
            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.Status); 
        }
    }
}