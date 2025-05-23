﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myMusicApi.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string add = @"
                CREATE PROCEDURE AddMusic
                    @Id NVARCHAR(450),
                    @Title NVARCHAR(450),
                    @ReleaseDate DATETIME2(7),
                    @Artist NVARCHAR(max),
                    @Rate INT
                AS
                BEGIN
                    INSERT INTO Musics (Id, Title, ReleaseDate, Artist, Rate)
                    VALUES (@Id,@Title, @ReleaseDate, @Artist, @Rate)
                END";
            migrationBuilder.Sql(add);
        }
        

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
              string remove = @"
                DROP PROCEDURE AddMusic"
                ;   
            migrationBuilder.Sql(remove);
        }
    }
}
