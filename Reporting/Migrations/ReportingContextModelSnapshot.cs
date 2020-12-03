using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Reporting.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneContact.API.Migrations
{
    [DbContext(typeof(ReportingContext))]
    public class ReportingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataModels.Report", m =>
            {
                m.Property<long>("Id")
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnType("bigint")
                   .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                m.Property<Guid>("UIID")
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnType("uniqueidentifier");

                m.Property<DateTimeOffset>("CreateDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime");

                m.Property<DateTimeOffset>("UpdateDate")
                    .HasColumnType("datetime");

                m.Property<bool>("IsDeleted")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit");

                m.Property<int>("Status")
                    .HasColumnType("int");

                m.Property<double>("Lattitude")
                       .HasColumnType("decimal(9,6)");

                m.Property<double>("Logitude")
                         .HasColumnType("decimal(9,6)");

                m.HasKey("Id");
                m.HasIndex("Id").IsUnique();

                m.ToTable("Report", "dbo");
            });


            modelBuilder.Entity("DataModels.ReportDetail", m =>
            {
                m.Property<long>("Id")
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnType("bigint")
                   .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                m.Property<Guid>("UIID")
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnType("uniqueidentifier");

                m.Property<DateTimeOffset>("CreateDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime");

                m.Property<DateTimeOffset>("UpdateDate")
                    .HasColumnType("datetime");

                m.Property<bool>("IsDeleted")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit");

                m.Property<long>("Report")
                    .HasColumnType("bigint");

                m.Property<int>("NearbyPersonCount")
                    .HasColumnType("int");

                m.Property<int>("NearbyPhoneNumberCount")
                    .HasColumnType("int");

                m.HasKey("Id");
                m.HasIndex("Id").IsUnique();
                m.HasIndex("Report");

                m.ToTable("Report", "RPT");
            });
        }
    }
}
