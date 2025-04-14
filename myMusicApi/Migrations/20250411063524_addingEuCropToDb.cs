using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myMusicApi.Migrations
{
    /// <inheritdoc />
    public partial class addingEuCropToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eu_Crop_Dicts",
                columns: table => new
                {
                    EU_CROP_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EU_CROP_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LANGUAGE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eu_Crop_Dicts");
        }
    }
}
