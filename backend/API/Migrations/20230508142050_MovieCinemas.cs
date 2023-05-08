using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class MovieCinemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_movie_cinemas_movies_movie_cinema_movie_cinema_id",
                table: "movie_cinemas_movies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_movie_cinema",
                table: "movie_cinema");

            migrationBuilder.RenameTable(
                name: "movie_cinema",
                newName: "movie_cinemas");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<DateTime>(
                name: "release_date",
                table: "movies",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                table: "actors",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Point>(
                name: "location",
                table: "movie_cinemas",
                type: "geometry",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_movie_cinemas",
                table: "movie_cinemas",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_movie_cinemas_movies_movie_cinemas_movie_cinema_id",
                table: "movie_cinemas_movies",
                column: "movie_cinema_id",
                principalTable: "movie_cinemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_movie_cinemas_movies_movie_cinemas_movie_cinema_id",
                table: "movie_cinemas_movies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_movie_cinemas",
                table: "movie_cinemas");

            migrationBuilder.DropColumn(
                name: "location",
                table: "movie_cinemas");

            migrationBuilder.RenameTable(
                name: "movie_cinemas",
                newName: "movie_cinema");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<DateTime>(
                name: "release_date",
                table: "movies",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                table: "actors",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "pk_movie_cinema",
                table: "movie_cinema",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_movie_cinemas_movies_movie_cinema_movie_cinema_id",
                table: "movie_cinemas_movies",
                column: "movie_cinema_id",
                principalTable: "movie_cinema",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
