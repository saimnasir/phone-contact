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
                name: "PERSON",
                schema: "PHC",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UIID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
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
               name: "CONTACTINFO",
               schema: "PHC",
               columns: table => new
               {
                   Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   UIID = table.Column<Guid>(nullable: false),
                   CreateDate = table.Column<DateTime>(nullable: false),
                   UpdateDate = table.Column<DateTime>(nullable: true),
                   IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
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
                       principalTable: "PERSON",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.NoAction);
               });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_Person",
                schema: "PHC",
                table: "CONTACTINFO",
                column: "Person");
            #endregion


            #region Person
            migrationBuilder.CreateTable(
                name: "LOCATION",
                schema: "PHC",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UIID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Latitude = table.Column<decimal>(nullable: false, precision:16, scale:6),
                    Longitude = table.Column<decimal>(nullable: false, precision: 16, scale: 6),
                    ContactInfo = table.Column<long>(nullable: false, precision: 16, scale: 6)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                    name: "FK_Location_ContactInfo_Id",
                    column: x => x.ContactInfo,
                    principalSchema: "PHC",
                    principalTable: "CONTACTINFO",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_ContactInfo",
                schema: "PHC",
                table: "LOCATION",
                column: "ContactInfo");
            #endregion

            generateStoreProcedures(migrationBuilder);

        }

        private void generateStoreProcedures(MigrationBuilder migrationBuilder)
        {
            generatePersonStoreProcedures(migrationBuilder);

            generateContactInfoStoreProcedures(migrationBuilder);
            generateLocationsStoreProcedures(migrationBuilder);
        }
         

        private static void generatePersonStoreProcedures(MigrationBuilder migrationBuilder)
        {
            var tableName = "PERSON";
            var schema = "PHC";
            var schemaTable = $"{schema}.{tableName}";

            var procedureName = $"{schema}.INS_{tableName}_SP";
            var insertProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName} 
(	 	
    @FirstName NVARCHAR(50),
	@MiddleName NVARCHAR(50) = NULL,
	@LastName NVARCHAR(50),
	@CompanyName NVARCHAR(100) = NULL
)
AS
BEGIN  
	INSERT INTO {schemaTable}
            (FirstName,
            MiddleName,
		    LastName,
		    CompanyName,
            UIID,
            CreateDate) 
	OUTPUT Inserted.Id
	SELECT  @FirstName,  
		    @MiddleName, 
		    @LastName, 
		    @CompanyName,
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
	@FirstName NVARCHAR(50),
	@MiddleName NVARCHAR(50) = NULL,
	@LastName NVARCHAR(50),
	@CompanyName NVARCHAR(100) = NULL
)
AS
BEGIN  

	UPDATE {schemaTable}
    SET 
	FirstName =@FirstName,
    MiddleName = @MiddleName,
	LastName = @LastName,
	CompanyName = @CompanyName,
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

        private void generateContactInfoStoreProcedures(MigrationBuilder migrationBuilder)
        {
            var tableName = "CONTACTINFO";
            var schema = "PHC";
            var schemaTable = $"{schema}.{tableName}";

            var procedureName = $"{schema}.INS_{tableName}_SP";
            var insertProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName} 
(	 
    @Person BIGINT,
	@InfoType BIGINT,
	@Information NVARCHAR(200)
)
AS
BEGIN  
	INSERT INTO {schemaTable}
            (Person,
            InfoType,
		    Information,
            UIID,
            CreateDate)  
	OUTPUT Inserted.Id
	SELECT  @Person,  
		    @InfoType, 
		    @Information,
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
    AND @Master IS NULL OR Person = @Master
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
	@Person BIGINT,
	@InfoType BIGINT,
	@Information NVARCHAR(200)
)
AS
BEGIN  

	UPDATE {schemaTable}
    SET 
	Person =@Person, 
    InfoType = @InfoType,
	Information = @Information,
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

            procedureName = $"{schema}.LST_{tableName}BYTYPE_SP";
            var listByTypeProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}
(	 
	@InfoType BIGINT
)
AS
BEGIN  
	SELECT * 
    FROM {schemaTable}
    WHERE IsDeleted = 0
    AND @InfoType IS NULL OR InfoType = @InfoType
END;  ";
            migrationBuilder.Sql(listByTypeProcedure);




            var calculateDistancesFunc = $@"
IF OBJECT_ID (N'PHC.CalculateGeographicDistances', N'FN') IS NOT NULL  
    DROP FUNCTION PHC.CalculateGeographicDistances;  
GO  
CREATE FUNCTION PHC.CalculateGeographicDistances(@lat1 DECIMAL(16,6), @long1 DECIMAL(16,6), @lat2 DECIMAL(16,6), @long2 DECIMAL(16,6))  
RETURNS FLOAT   
AS   
-- Returns the stock level for the product.  
BEGIN  
    DECLARE @result DECIMAL(16,6);  
	DECLARE @geo1 GEOGRAPHY = GEOGRAPHY::Point(@lat1, @long1, 4326),
        @geo2 GEOGRAPHY = GEOGRAPHY::Point(@lat2, @long2, 4326)
	  
	select @result =@geo1.STDistance(@geo2) 	 
    RETURN @result;  
END;
";

            migrationBuilder.Sql(calculateDistancesFunc);


            var nearbyCounstProcedure = $@"

IF OBJECT_ID('PHC.SEL_NEARBYCOUNTS_SP') IS NULL
    EXEC('CREATE PROCEDURE PHC.SEL_NEARBYCOUNTS_SP AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE PHC.SEL_NEARBYCOUNTS_SP
(	 
	@Radius decimal(16,6),
    @Latitude decimal(16,6),
	@Longitude decimal(16,6)
)
AS
BEGIN  
	BEGIN TRANSACTION  
	DECLARE @NearByPersonCount INT = 0, @NearByPhoneNumberCount INT = 0; 

	  SELECT
		PHC.CalculateGeographicDistances(@Latitude, @Longitude, Latitude, Longitude) AS DISTANCE,
		L.Id AS Location,
		L.ContactInfo 
	  INTO #DISTANCES
	  FROM PHC.LOCATION L
	  INNER JOIN PHC.CONTACTINFO C ON C.ID = L.ContactInfo
	  WHERE C.InfoType = 3 -- 3: Location 
 
	  SELECT
		P.* INTO #NEARBYPERSON
	  FROM #DISTANCES D
	  INNER JOIN PHC.CONTACTINFO C
		ON C.Id = D.ContactInfo
		INNER JOIN PHC.PERSON P
		  ON P.Id = C.Person
	  WHERE D.DISTANCE <= @Radius
  
		;WITH NearbyPhoneNumbers  AS (
		  SELECT
			C.*
		  FROM #NEARBYPERSON P
		  INNER JOIN PHC.CONTACTINFO C
			ON C.Person = P.Id
		  WHERE C.InfoType = 1 -- 1: PhoneNumber
		)

		SELECT
			@NearByPhoneNumberCount = COUNT(*)
		FROM NearbyPhoneNumbers P

		SELECT
			@NearByPersonCount = COUNT(*)
		FROM #NEARBYPERSON P


		DROP TABLE #DISTANCES

		DROP TABLE #NEARBYPERSON

		Select @NearByPersonCount as NearByPersonCount ,@NearByPhoneNumberCount as NearByPhoneNumberCount
	COMMIT TRANSACTION

END
";

            migrationBuilder.Sql(nearbyCounstProcedure);

        }

        private void generateLocationsStoreProcedures(MigrationBuilder migrationBuilder)
        {
            var tableName = "LOCATION";
            var schema = "PHC";
            var schemaTable = $"{schema}.{tableName}";

            var procedureName = $"{schema}.INS_{tableName}_SP";
            var insertProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName} 
(	 	
    @ContactInfo BIGINT,
    @Latitude decimal(9,6),
	@Longitude decimal(9,6)
)
AS
BEGIN  
	INSERT INTO {schemaTable}
            (ContactInfo,
            Latitude,
            Longitude,
		    UIID,
            CreateDate) 
	OUTPUT Inserted.Id
	SELECT  @ContactInfo,
            @Latitude,  
		    @Longitude, 
            NEWID(),
            GETDATE()
END;  ";
            migrationBuilder.Sql(insertProcedure);

            procedureName = $"{schema}.LST_{tableName}_SP";
            var listProcedure = $@"
IF OBJECT_ID('{procedureName}') IS NULL
    EXEC('CREATE PROCEDURE {procedureName} AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE {procedureName}(
    @Master BIGINT = NULL
)
AS
BEGIN  
	SELECT * 
    FROM {schemaTable}
    WHERE IsDeleted = 0
    AND @Master IS NULL OR @Master = ContactInfo
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
	@ContactInfo BIGINT,
    @Latitude decimal(9,6),
	@Longitude decimal(9,6)
)
AS
BEGIN  

	UPDATE {schemaTable}
    SET 
	ContactInfo = @ContactInfo,
	Latitude = @Latitude,
    Longitude = @Longitude,
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
               name: "PERSON",
               schema: "PHC");

            migrationBuilder.DropTable(
                name: "CONTACTINFO",
                schema: "PHC");

            migrationBuilder.DropTable(
                name: "LOCATION",
                schema: "PHC");
        }
    }
}
