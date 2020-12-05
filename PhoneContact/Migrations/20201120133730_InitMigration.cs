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
                    UIID = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false, defaultValue: DateTime.Now),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
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
                   UIID = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                   CreateDate = table.Column<DateTimeOffset>(nullable: false, defaultValue: DateTime.Now),
                   UpdateDate = table.Column<DateTimeOffset>(nullable: true),
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
            generateStoreProcedures(migrationBuilder);

            #region commented 
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
            #endregion

        }

        private void generateStoreProcedures(MigrationBuilder migrationBuilder)
        {
            generatePersonStoreProcedures(migrationBuilder);

            generateContactInfoStoreProcedures(migrationBuilder);
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
		    CompanyName) 
	OUTPUT Inserted.Id
	SELECT  @FirstName,  
		    @MiddleName, 
		    @LastName, 
		    @CompanyName
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
		    Information) 
	OUTPUT Inserted.Id
	SELECT  @Person,  
		    @InfoType, 
		    @Information
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
