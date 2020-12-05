using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PhoneContact.Extensions;
using PhoneContact.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneContact.API.Migrations
{
    [DbContext(typeof(PhoneContactContext))]
    [Migration("20201120133730_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
              .HasAnnotation("ProductVersion", "1.0.2")
              .HasAnnotation("Relational:MaxIdentifierLength", 128)
              .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataModels.Person", m =>
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

                m.Property<DateTime>("CreateDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime");

                m.Property<DateTime>("UpdateDate")
                    .HasColumnType("datetime");

                m.Property<bool>("IsDeleted")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit");

                m.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                m.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                m.Property<string>("MiddleName")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                m.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                m.Property<string>("CompanyName")
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(200);

                m.HasKey("Id");
                m.HasIndex("Id").IsUnique();

                m.ToTable("PERSON", "PHC");
            });

            modelBuilder.Entity("DataModels.ContactInfo", m =>
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

                m.Property<DateTime>("CreateDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime");

                m.Property<DateTime>("UpdateDate")
                    .HasColumnType("datetime");


                m.Property<bool>("IsDeleted")
                    .HasColumnType("bit");

                m.Property<long>("Person")
                    .IsRequired()
                    .HasColumnType("bigint");

                m.Property<int>("InfoType")
                    .HasColumnType("int");

                m.Property<string>("Information")
                    .IsRequired()
                    .HasColumnType("nvarchar(200)")
                    .HasMaxLength(1000);

                m.HasKey("Id");
                m.HasIndex("Id").IsUnique();

                m.HasIndex("Person");

                m.ToTable("CONTACTINFO", "PHC");
            });

            modelBuilder.Entity("DataModels.Location", m =>
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

                m.Property<DateTime>("CreateDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime");

                m.Property<DateTime>("UpdateDate")
                    .HasColumnType("datetime");

                m.Property<bool>("IsDeleted")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit"); 

                m.Property<decimal>("Latitude")
                    .IsRequired()
                    .HasColumnType("decimal(9,6)");

                m.Property<decimal>("Longitude")
                    .IsRequired()
                    .HasColumnType("decimal(9,6)");

                m.Property<long>("ContactInfo")
                    .IsRequired()
                    .HasColumnType("bigint");

                m.HasKey("Id");
                m.HasIndex("Id").IsUnique();

                m.HasIndex("ContactInfo");

                m.ToTable("LOCATION", "PHC");
            });           
        }

    }
}
