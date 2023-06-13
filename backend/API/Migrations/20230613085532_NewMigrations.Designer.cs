﻿// <auto-generated />
using System;
using API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230613085532_NewMigrations")]
    partial class NewMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("biography");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_actors");

                    b.ToTable("actors", (string)null);
                });

            modelBuilder.Entity("API.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("API.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("InCinemas")
                        .HasColumnType("boolean")
                        .HasColumnName("in_cinemas");

                    b.Property<string>("Poster")
                        .HasColumnType("text")
                        .HasColumnName("poster");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("release_date");

                    b.Property<string>("Summary")
                        .HasColumnType("text")
                        .HasColumnName("summary");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("title");

                    b.Property<string>("Trailer")
                        .HasColumnType("text")
                        .HasColumnName("trailer");

                    b.HasKey("Id")
                        .HasName("pk_movies");

                    b.ToTable("movies", (string)null);
                });

            modelBuilder.Entity("API.Models.MovieCinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Point>("Location")
                        .IsRequired()
                        .HasColumnType("geometry")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_movie_cinemas");

                    b.ToTable("movie_cinemas", (string)null);
                });

            modelBuilder.Entity("API.Models.MovieCinemasMovies", b =>
                {
                    b.Property<int>("MovieCinemaId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_cinema_id");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.HasKey("MovieCinemaId", "MovieId")
                        .HasName("pk_movie_cinemas_movies");

                    b.HasIndex("MovieId")
                        .HasDatabaseName("ix_movie_cinemas_movies_movie_id");

                    b.ToTable("movie_cinemas_movies", (string)null);
                });

            modelBuilder.Entity("API.Models.MoviesActors", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("integer")
                        .HasColumnName("actor_id");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.Property<string>("Character")
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("character");

                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int>("Order")
                        .HasColumnType("integer")
                        .HasColumnName("order");

                    b.HasKey("ActorId", "MovieId")
                        .HasName("pk_movies_actors");

                    b.HasIndex("MovieId")
                        .HasDatabaseName("ix_movies_actors_movie_id");

                    b.ToTable("movies_actors", (string)null);
                });

            modelBuilder.Entity("API.Models.MoviesCategories", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.HasKey("CategoryId", "MovieId")
                        .HasName("pk_movies_categories");

                    b.HasIndex("MovieId")
                        .HasDatabaseName("ix_movies_categories_movie_id");

                    b.ToTable("movies_categories", (string)null);
                });

            modelBuilder.Entity("API.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.Property<int>("Rate")
                        .HasColumnType("integer")
                        .HasColumnName("rate");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_ratings");

                    b.HasIndex("MovieId")
                        .HasDatabaseName("ix_ratings_movie_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_ratings_user_id");

                    b.ToTable("ratings", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_role_claims_role_id");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_claims_user_id");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_asp_net_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_logins_user_id");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_asp_net_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_user_roles_role_id");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_asp_net_user_tokens");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("API.Models.MovieCinemasMovies", b =>
                {
                    b.HasOne("API.Models.MovieCinema", "MovieCinema")
                        .WithMany()
                        .HasForeignKey("MovieCinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movie_cinemas_movies_movie_cinemas_movie_cinema_id");

                    b.HasOne("API.Models.Movie", "Movie")
                        .WithMany("MovieCinemasMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movie_cinemas_movies_movies_movie_id");

                    b.Navigation("Movie");

                    b.Navigation("MovieCinema");
                });

            modelBuilder.Entity("API.Models.MoviesActors", b =>
                {
                    b.HasOne("API.Models.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movies_actors_actors_actor_id");

                    b.HasOne("API.Models.Movie", "Movie")
                        .WithMany("MoviesActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movies_actors_movies_movie_id");

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("API.Models.MoviesCategories", b =>
                {
                    b.HasOne("API.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movies_categories_categories_category_id");

                    b.HasOne("API.Models.Movie", "Movie")
                        .WithMany("MoviesCategories")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_movies_categories_movies_movie_id");

                    b.Navigation("Category");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("API.Models.Rating", b =>
                {
                    b.HasOne("API.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_ratings_movies_movie_id");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_ratings_users_user_id");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id");
                });

            modelBuilder.Entity("API.Models.Movie", b =>
                {
                    b.Navigation("MovieCinemasMovies");

                    b.Navigation("MoviesActors");

                    b.Navigation("MoviesCategories");
                });
#pragma warning restore 612, 618
        }
    }
}