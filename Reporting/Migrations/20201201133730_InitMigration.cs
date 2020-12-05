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
                name: "Report".ToUpper(),
                schema: "RPT",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UIID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Status = table.Column<int>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: false, precision: 16, scale: 6),
                    Radius = table.Column<decimal>(nullable: false,  precision: 16, scale: 6),
                    Longitude = table.Column<decimal>(nullable: false, precision: 16, scale: 6)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                });

            #endregion

            #region ReportDetail

            migrationBuilder.CreateTable(
            name: "REPORTDETAIL",
            schema: "RPT",
            columns: table => new
            {
                Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                UIID = table.Column<Guid>(nullable: false),
                CreateDate = table.Column<DateTime>(nullable: false),
                UpdateDate = table.Column<DateTime>(nullable: true),
                IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                Report = table.Column<long>(nullable: false),
                NearbyPersonCount = table.Column<int>(nullable: false),
                NearbyPhoneNumberCount = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReportDetail", x => x.Id);
                table.ForeignKey(
                    name: "FK_ReportDetail_Report_Id",
                    column: x => x.Report,
                    principalSchema: "RPT",
                    principalTable: "Report".ToUpper(),
                    principalColumn: "Id",
                    onDelete: ReferentialAction.NoAction);
            });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetail_Report",
                schema: "RPT",
                table: "REPORTDETAIL",
                column: "Report");

            #endregion

            generateStoreProcedures(migrationBuilder);
             
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
    @Radius decimal(16,6),
    @Latitude decimal(16,6),
	@Longitude decimal(16,6),
	@Status INT
)
AS
BEGIN  
	INSERT INTO {schemaTable}
            (Radius,
            Latitude,
            Longitude,
		    Status,
		    UIID,
            CreateDate) 
	OUTPUT Inserted.Id
	SELECT  @Radius,
            @Latitude,  
		    @Longitude, 
		    @Status,
            NEWID(),
            GETDATE()
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
	@Radius decimal(16,6),
    @Latitude decimal(16,6),
	@Longitude decimal(16,6),
	@Status INT
)
AS
BEGIN  

	UPDATE {schemaTable}
    SET 
	Radius = @Radius,
	Latitude = @Latitude,
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


            procedureName = $"{schema}.LST_{tableName}SPENDING_SP";
            var pendingListProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}
AS
BEGIN  
	SELECT * 
    FROM {schemaTable}
    WHERE IsDeleted = 0 AND Status IN (1, 4) -- requested and failed
	 
END
";
            migrationBuilder.Sql(pendingListProcedure);

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
		    NearbyPhoneNumberCount,
            UIID,
            CreateDate)  
	OUTPUT Inserted.Id
	SELECT  @Report,  
 		    @NearbyPersonCount, 
		    @NearbyPhoneNumberCount,
            NEWID(),
            GETDATE()
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
               name: "Report".ToUpper(),
               schema: "RPT");

            migrationBuilder.DropTable(
                name: "REPORTDETAIL",
                schema: "RPT");

            //migrationBuilder.DropTable(
            //    name: "Genres",
            //    schema: "RPT");
        }
    }
}
