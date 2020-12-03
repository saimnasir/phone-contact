using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhoneContact.Repositories.SchemaDefinitions
{
    public class ContactInfoEntitySchemaConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfo", PhoneContactContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.UIID);
            builder.Property(p => p.CreateDate).IsRequired();
            builder.Property(p => p.UpdateDate);
            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.Person).IsRequired();
            builder.Property(p => p.InfoType).IsRequired();
            builder.Property(p => p.Information).IsRequired().HasMaxLength(200);
        }
    }
}