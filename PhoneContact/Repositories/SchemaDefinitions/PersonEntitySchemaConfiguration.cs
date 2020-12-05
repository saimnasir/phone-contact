using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhoneContact.Repositories.SchemaDefinitions
{
    public class PersonEntitySchemaConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", PhoneContactContext.DEFAULT_SCHEMA);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UIID).IsRequired();
            builder.Property(p => p.CreateDate).IsRequired();
            builder.Property(p => p.UpdateDate);
            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.MiddleName).HasMaxLength(200);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.CompanyName).IsRequired().HasMaxLength(200);
        }
    }
}