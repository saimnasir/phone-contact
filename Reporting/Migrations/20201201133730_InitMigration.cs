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
                    UIID = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false, defaultValue: DateTime.Now),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
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
                UIID = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                CreateDate = table.Column<DateTimeOffset>(nullable: false, defaultValue: DateTime.Now),
                UpdateDate = table.Column<DateTimeOffset>(nullable: true),
                IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
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

            generateStoreProcedures(migrationBuilder);

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

        private void generateStoreProcedures(MigrationBuilder migrationBuilder)
        {
            generateReportStoreProcedures(migrationBuilder);

            generateReportDetailInfoStoreProcedures(migrationBuilder);
        }

        private void generateReportStoreProcedures(MigrationBuilder migrationBuilder)
        {
            var tableName = "REPORT";
            var schema = "RPT";
            var schemaTable = $"{schema}.{tableName}";

            var procedureName = $"{schema}.INS_{tableName}_SP";
            var insertProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName} 
(	 	
    @Lattitude decimal(9,6),
	@Longitude decimal(9,6),
	@Status INT
)
AS
BEGIN  
	INSERT INTO {schemaTable}
            (Lattitude,
            Longitude,
		    Status) 
	OUTPUT Inserted.Id
	SELECT  @Lattitude,  
		    @Longitude, 
		    @Status
END;  ";
            migrationBuilder.Sql(insertProcedure);

            procedureName = $"{schema}.LST_{tableName}_SP";
            var listProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}
AS
BEGIN  
	SELECT * 
    FROM {schemaTable}
    WHERE IsDeleted = 0
END;  ";
            migrationBuilder.Sql(listProcedure);

            procedureName = $"{schema}.SEL_{tableName}_SP";
            var selectProcedureName = procedureName;
            var selectProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}(
    @Id BIGINT
)
AS
BEGIN  
	SELECT * 
    FROM {schemaTable}
    WHERE IsDeleted = 0 AND Id = @Id
END;  ";
            migrationBuilder.Sql(selectProcedure);

            procedureName = $"{schema}.DEL_{tableName}_SP";
            var deleteProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}
(	 
	@Id BIGINT
)
AS
BEGIN   

	DECLARE @IsDeleted BIT = 0;
	UPDATE {schemaTable} 
		Set IsDeleted = 1
	Where Id = @Id

	IF @@ROWCOUNT > 0 
	BEGIN
		SET @IsDeleted = 1;
	END
	
	SELECT @IsDeleted
END; ";
            migrationBuilder.Sql(deleteProcedure);

            procedureName = $"{schema}.UPD_{tableName}_SP";
            var updateProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}
(	 
	@Id BIGINT,
	@Lattitude decimal(9,6),
	@Longitude decimal(9,6),
	@Status INT
)
AS
BEGIN  

	UPDATE {schemaTable}
    SET 
	Lattitude =@Lattitude,
    Longitude = @Longitude,
	Status = @Status,
    UpdateDate = GETDATE()
	WHERE @Id = Id

	IF @@ROWCOUNT >0
	BEGIN
		EXEC {selectProcedureName} @Id
	END
	ELSE
	BEGIN
		THROW 50001, 'Nothing Updated', 1;
	END
END
";
            migrationBuilder.Sql(updateProcedure);

        }

        private void generateReportDetailInfoStoreProcedures(MigrationBuilder migrationBuilder)
        {
            var tableName = "REPORTDETAIL";
            var schema = "RPT";
            var schemaTable = $"{schema}.{tableName}";

            var procedureName = $"{schema}.INS_{tableName}_SP";
            var insertProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName} 
(	 	
    @Report BIGINT,
	@NearbyPersonCount INT,
	@NearbyPhoneNumberCount INT
)
AS
BEGIN  
	INSERT INTO {schemaTable}
            (Report,
            NearbyPersonCount,
		    NearbyPhoneNumberCount) 
	OUTPUT Inserted.Id
	SELECT  @Report,  
 		    @NearbyPersonCount, 
		    @NearbyPhoneNumberCount
END;  ";
            migrationBuilder.Sql(insertProcedure);

            procedureName = $"{schema}.LST_{tableName}_SP";
            var listProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}
(	 
	@Master BIGINT = NULL
)
AS
BEGIN  
	SELECT * 
    FROM {schemaTable}
    WHERE IsDeleted = 0
    AND @Master IS NULL OR Report = @Master
END;  ";
            migrationBuilder.Sql(listProcedure);

            procedureName = $"{schema}.SEL_{tableName}_SP";
            var selectProcedureName = procedureName;
            var selectProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}(
    @Id BIGINT
)
AS
BEGIN  
	SELECT * 
    FROM {schemaTable}
    WHERE IsDeleted = 0 AND Id = @Id
END;  ";
            migrationBuilder.Sql(selectProcedure);

            procedureName = $"{schema}.DEL_{tableName}_SP";
            var deleteProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}
(	 
	@Id BIGINT
)
AS
BEGIN   

	DECLARE @IsDeleted BIT = 0;
	UPDATE PHC.PERSON 
		Set IsDeleted = 1
	Where Id = @Id

	IF @@ROWCOUNT > 0 
	BEGIN
		SET @IsDeleted = 1;
	END
	
	SELECT @IsDeleted
END; ";
            migrationBuilder.Sql(deleteProcedure);

            procedureName = $"{schema}.UPD_{tableName}_SP";
            var updateProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}
(	 
	@Id BIGINT,
	@Report BIGINT,
	@NearbyPersonCount INT,
	@NearbyPhoneNumberCount INT
)
AS
BEGIN  

	UPDATE {schemaTable}
    SET 
	Report =@Report,
    NearbyPersonCount = @NearbyPersonCount,
	NearbyPhoneNumberCount = @NearbyPhoneNumberCount,
    UpdateDate = GETDATE()
	WHERE @Id = Id

	IF @@ROWCOUNT >0
	BEGIN
		EXEC {selectProcedureName} @Id
	END
	ELSE
	BEGIN
		THROW 50001, 'Nothing Updated', 1;
	END
END
";
            migrationBuilder.Sql(updateProcedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Report",
               schema: "RPT");

            migrationBuilder.DropTable(
                name: "ReportDetail",
                schema: "RPT");

            //migrationBuilder.DropTable(
            //    name: "Genres",
            //    schema: "RPT");
        }
    }
}
