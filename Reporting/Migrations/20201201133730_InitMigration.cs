using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reporting.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "RPT");

            #region Report

            migrationBuilder.CreateTable(
                name: "Report",
                schema: "RPT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UIID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Lattitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                });

            #endregion

            #region ReportDetail

            migrationBuilder.CreateTable(
            name: "ReportDetail",
            schema: "RPT",
            columns: table => new
            {
                Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                UIID = table.Column<Guid>(nullable: false),
                CreateDate = table.Column<DateTimeOffset>(nullable: false),
                UpdateDate = table.Column<DateTimeOffset>(nullable: true),
                IsDeleted = table.Column<bool>(nullable: false),
                Report = table.Column<long>(nullable: false),
                NearbyReportCount = table.Column<int>(nullable: false),
                NearbyPhoneNumberCount = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReportDetail", x => x.Id);
                table.ForeignKey(
                    name: "FK_ReportDetail_Report_Id",
                    column: x => x.Report,
                    principalSchema: "RPT",
                    principalTable: "Report",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.NoAction);
            });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetail_Report",
                schema: "RPT",
                table: "ReportDetail",
                column: "Report");

            #endregion

            #region Commented
            //migrationBuilder.CreateTable(
            //    name: "Genres",
            //    schema: "RPT",
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
            //    schema: "RPT",
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
            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Report",
               schema: "RPT");

            //migrationBuilder.DropTable(
            //    name: "Authors",
            //    schema: "RPT");

            //migrationBuilder.DropTable(
            //    name: "Genres",
            //    schema: "RPT");
        }
    }
}
