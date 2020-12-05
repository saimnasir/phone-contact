using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Reporting.Repositories;
using System;

namespace Reporting.Migrations
{
    [DbContext(typeof(ReportingContext))]
    [Migration("20201201133730_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                m.Property<DateTime>("CreateDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime");

                m.Property<DateTime>("UpdateDate")
                    .HasColumnType("datetime");

                m.Property<bool>("IsDeleted")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bit");

                m.Property<double>("Radius")
                  .HasColumnType("decimal(9,6)");

                m.Property<int>("Status")
                    .HasColumnType("int");

                m.Property<double>("Latitude")
                       .HasColumnType("decimal(9,6)");

            
                m.Property<double>("Logitude")
                         .HasColumnType("decimal(9,6)");

                m.HasKey("Id");
                m.HasIndex("Id").IsUnique();

                m.ToTable("Report".ToUpper(), "dbo");
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

                m.Property<DateTime>("CreateDate")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime");

                m.Property<DateTime>("UpdateDate")
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

                m.ToTable("Report".ToUpper(), "RPT");
            });

            //modelBuilder.Entity("BookShop.Domain.Entities.Book", b =>
            //{
            //    b.Property<Guid>("Id")
            //        .ValueGeneratedOnAdd()
            //        .HasColumnType("uniqueidentifier");

            //    b.Property<Guid>("AuthorId")
            //        .HasColumnType("uniqueidentifier");

            //    b.Property<int>("AvailableStock")
            //        .HasColumnType("int");

            //    b.Property<string>("Description")
            //        .IsRequired()
            //        .HasColumnType("nvarchar(1000)")
            //        .HasMaxLength(1000);

            //    b.Property<string>("Format")
            //        .HasColumnType("nvarchar(max)");

            //    b.Property<Guid>("GenreId")
            //        .HasColumnType("uniqueidentifier");

            //    b.Property<bool>("IsInactive")
            //        .HasColumnType("bit");

            //    b.Property<string>("LabelName")
            //        .HasColumnType("nvarchar(max)");

            //    b.Property<string>("Name")
            //        .IsRequired()
            //        .HasColumnType("nvarchar(max)");

            //    b.Property<string>("PictureUri")
            //        .HasColumnType("nvarchar(max)");

            //    b.Property<string>("Price")
            //        .HasColumnType("nvarchar(max)");

            //    b.Property<DateTime>("ReleaseDate")
            //        .HasColumnType("datetimeoffset");

            //    b.HasKey("Id");

            //    b.HasIndex("AuthorId");

            //    b.HasIndex("GenreId");

            //    b.ToTable("Books", "bookshop");
            //});

            //modelBuilder.Entity("BookShop.Domain.Entities.Genre", b =>
            //{
            //    b.Property<Guid>("GenreId")
            //        .ValueGeneratedOnAdd()
            //        .HasColumnType("uniqueidentifier");

            //    b.Property<string>("GenreDescription")
            //        .IsRequired()
            //        .HasColumnType("nvarchar(1000)")
            //        .HasMaxLength(1000);

            //    b.HasKey("GenreId");

            //    b.ToTable("Genres", "bookshop");
            //});

            //modelBuilder.Entity("BookShop.Domain.Entities.Book", b =>
            //{
            //    b.HasOne("BookShop.Domain.Entities.Author", "Author")
            //        .WithMany("Books")
            //        .HasForeignKey("AuthorId")
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .IsRequired();

            //    b.HasOne("BookShop.Domain.Entities.Genre", "Genre")
            //        .WithMany("Books")
            //        .HasForeignKey("GenreId")
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .IsRequired();
            //});
        }

    }
}
