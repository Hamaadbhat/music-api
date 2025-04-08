using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myMusicApi.Migrations
{
    /// <inheritdoc />
    public partial class getAllMusic_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            String getAll = @"
                CREATE PROCEDURE GetAllMusic
                AS
                BEGIN
                    SELECT * FROM Musics
                END
            ";
            migrationBuilder.Sql(getAll);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            String getAll = @"
                DROP PROCEDURE GetAllMusic
            ";
            migrationBuilder.Sql(getAll);

        }
    }
}
