using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class MovieAndOthers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_movies_categories",
                table: "movies_categories");

            migrationBuilder.DropIndex(
                name: "ix_movies_categories_category_id",
                table: "movies_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_movies_actors",
                table: "movies_actors");

            migrationBuilder.DropIndex(
                name: "ix_movies_actors_actor_id",
                table: "movies_actors");

            migrationBuilder.DropPrimaryKey(
                name: "pk_movie_cinemas_movies",
                table: "movie_cinemas_movies");

            migrationBuilder.DropIndex(
                name: "ix_movie_cinemas_movies_movie_cinema_id",
                table: "movie_cinemas_movies");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "movies_categories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "movies_actors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "movie_cinemas_movies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_movies_categories",
                table: "movies_categories",
                columns: new[] { "category_id", "movie_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_movies_actors",
                table: "movies_actors",
                columns: new[] { "actor_id", "movie_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_movie_cinemas_movies",
                table: "movie_cinemas_movies",
                columns: new[] { "movie_cinema_id", "movie_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_movies_categories",
                table: "movies_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_movies_actors",
                table: "movies_actors");

            migrationBuilder.DropPrimaryKey(
                name: "pk_movie_cinemas_movies",
                table: "movie_cinemas_movies");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "movies_categories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "movies_actors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "movie_cinemas_movies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_movies_categories",
                table: "movies_categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_movies_actors",
                table: "movies_actors",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_movie_cinemas_movies",
                table: "movie_cinemas_movies",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_movies_categories_category_id",
                table: "movies_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_movies_actors_actor_id",
                table: "movies_actors",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "ix_movie_cinemas_movies_movie_cinema_id",
                table: "movie_cinemas_movies",
                column: "movie_cinema_id");
        }
    }
}
