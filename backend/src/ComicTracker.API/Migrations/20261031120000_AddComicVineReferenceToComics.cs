using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comicTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddComicVineReferenceToComics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComicVineId",
                table: "Comics",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ComicVineLastSync",
                table: "Comics",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComicVineId",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "ComicVineLastSync",
                table: "Comics");
        }
    }
}
