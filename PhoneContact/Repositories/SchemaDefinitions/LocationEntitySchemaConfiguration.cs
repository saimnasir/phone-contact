using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneContact.Repositories.SchemaDefinitions
{
    public class LocationEntitySchemaConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location", PhoneContactContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.Id);
            builder.Property(p => p.UIID).IsRequired();
            builder.Property(p => p.CreateDate).IsRequired();
            builder.Property(p => p.UpdateDate);
            builder.Property(p => p.IsDeleted);
        }
    }
}
