using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneContact.API.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "PHC");

            #region Person
            migrationBuilder.CreateTable(
                name: "Person",
                schema: "PHC",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UIID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });
            #endregion

            #region ContactInfo

            migrationBuilder.CreateTable(
               name: "ContactInfo",
               schema: "PHC",
               columns: table => new
               {
                   Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   UIID = table.Column<Guid>(nullable: false),
                   CreateDate = table.Column<DateTimeOffset>(nullable: false),
                   UpdateDate = table.Column<DateTimeOffset>(nullable: true),
                   IsDeleted = table.Column<bool>(nullable: false),
                   Person = table.Column<long>(nullable: false),
                   InfoType = table.Column<int>(nullable: false),
                   Information = table.Column<string>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_ContactInfo", x => x.Id);
                   table.ForeignKey(
                       name: "FK_ContactInfo_Person_Id",
                       column: x => x.Person,
                       principalSchema: "PHC",
                       principalTable: "Person",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.NoAction);
               });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_Person",
                schema: "PHC",
                table: "ContactInfo",
                column: "Person");
            #endregion
            //migrationBuilder.CreateTable(
            //    name: "Genres",
            //    schema: "PHC",
            //    columns: table => new
            //    {
            //        GenreId = table.Column<Guid>(nullable: false),
            //        GenreDescription = table.Column<string>(maxLength: 1000, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Genres", x => x.GenreId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Books",
            //    schema: "PHC",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        Name = table.Column<string>(nullable: false),
            //        Description = table.Column<string>(maxLength: 1000, nullable: false),
            //        LabelName = table.Column<string>(nullable: true),
            //        Price = table.Column<string>(nullable: true),
            //        PictureUri = table.Column<string>(nullable: true),
            //        ReleaseDate = table.Column<DateTimeOffset>(nullable: false),
            //        Format = table.Column<string>(nullable: true),
            //        AvailableStock = table.Column<int>(nullable: false),
            //        GenreId = table.Column<Guid>(nullable: false),
            //        AuthorId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Books", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Books_Authors_AuthorId",
            //            column: x => x.AuthorId,
            //            principalSchema: "bookshop",
            //            principalTable: "Authors",
            //            principalColumn: "AuthorId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Books_Genres_GenreId",
            //            column: x => x.GenreId,
            //            principalSchema: "bookshop",
            //            principalTable: "Genres",
            //            principalColumn: "GenreId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Books_AuthorId",
            //    schema: "bookshop",
            //    table: "Books",
            //    column: "AuthorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Books_GenreId",
            //    schema: "bookshop",
            //    table: "Books",
            //    column: "GenreId");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Person",
               schema: "PHC");

            migrationBuilder.DropTable(
                name: "ContactInfo",
                schema: "PHC");

            //migrationBuilder.DropTable(
            //    name: "Genres",
            //    schema: "PHC");
        }
    }
}
