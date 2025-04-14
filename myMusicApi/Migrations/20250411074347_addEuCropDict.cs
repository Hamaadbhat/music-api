using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myMusicApi.Migrations
{
    /// <inheritdoc />
    public partial class addEuCropDict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string add = @" CREATE PROCEDURE InsertEuCrop
                @EU_CROP_CODE NVARCHAR(MAX),
                @EU_CROP_NAME NVARCHAR(MAX),
                @LANGUAGE NVARCHAR(MAX)
            AS
            BEGIN
                    INSERT INTO eu_Crop_Dicts(EU_CROP_CODE, EU_CROP_NAME, LANGUAGE)
                    VALUES(@EU_CROP_CODE, @EU_CROP_NAME, @LANGUAGE)
            END";
            migrationBuilder.Sql(add);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string remove = @"
                DROP PROCEDURE InsertEuCrop"
              ;
            migrationBuilder.Sql(remove);

        }
    }
}
