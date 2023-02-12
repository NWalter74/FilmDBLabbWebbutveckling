using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FDBL.Membership.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Released = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Free = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FilmUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageLargeUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageSimilarUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DirectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Films_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmGenres",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGenres", x => new { x.FilmId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_FilmGenres_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimilarFilms",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    SimilarFilmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarFilms", x => new { x.FilmId, x.SimilarFilmId });
                    table.ForeignKey(
                        name: "FK_SimilarFilms_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SimilarFilms_Films_SimilarFilmId",
                        column: x => x.SimilarFilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1001, "Brad Peyton" },
                    { 1002, "Jordan Vogt-Roberts" },
                    { 1003, "Brad Peyton" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 101, "Action" },
                    { 102, "Sci-Fi" },
                    { 103, "Adventure" },
                    { 104, "Thriller" },
                    { 105, "Fantasy" },
                    { 106, "Comedy" },
                    { 107, "Animation" },
                    { 108, "Family" }
                });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "DirectorId", "FilmUrl", "Free", "ImageLargeUrl", "ImageSimilarUrl", "ImageUrl", "Released", "Title" },
                values: new object[,]
                {
                    { 11, "När tre olika djur smittas av en farlig patogen som gör dem till jättemonster slår sig en primatolog ihop med en genetiker för att hindra dem från att ödelägga Chicago.", 1001, "https://youtu.be/coOKvrsmQiI", false, "/images/Rampage_large.png", "/images/Rampage_sim.png", "/images/Rampage.png", new DateTime(2018, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rampage" },
                    { 12, "När den okända San Andreasförkastningen slutligen ger vika, vilket utlöser en jordbävning av magnitud 9 i Kalifornien, tar sig en helikopterräddare och hans förfrämligade fru från Los Angeles till San Francisco för att rädda sin enda dotter.", 1001, "https://youtu.be/23VflsU3kZE", false, "/images/SanAndreas_large.png", "/images/SanAndreas_sim.png", "/images/SanAndreas.png", new DateTime(2015, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "San Andreas" },
                    { 13, "En brokig skara forskare, soldater och äventyrare samlas för att utforska en vacker men förrädisk ö i Stilla havet, omedvetna om att de inkräktar på den mytiska Kongs territorium.", 1002, "https://youtu.be/dBLdPIp-BuY", false, "/images/KonSkullIsland_large.png", "/images/KonSkullIsland_sim.png", "/images/KonSkullIsland.png", new DateTime(2017, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kong Skull Island" },
                    { 14, "Den unge Sean Anderson får ett kodat nödanrop från en otrolig plats: en outforskad mystisk ö. Så inleds äventyret i uppföljaren till succén ”Journey to the Center of the Earth”.", 1001, "https://youtu.be/1Q2LVXlHKS8", false, "/images/Journey2_large.png", "/images/Journey2_sim.png", "/images/Journey2.png", new DateTime(2012, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Journey2" },
                    { 15, "Maltazard har fångat Arthur i Minimojernas rike och har påbörjat kampen för att ta över människornas värld.", 1003, "https://youtu.be/QWHsbDbLCJM", false, "/images/Arthur_large.png", "/images/Arthur_sim.png", "/images/Arthur.png", new DateTime(2011, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arthur The War Of Two Worlds" }
                });

            migrationBuilder.InsertData(
                table: "FilmGenres",
                columns: new[] { "FilmId", "GenreId" },
                values: new object[,]
                {
                    { 11, 101 },
                    { 11, 102 },
                    { 11, 103 },
                    { 12, 101 },
                    { 12, 103 },
                    { 12, 104 },
                    { 13, 101 },
                    { 13, 103 },
                    { 13, 105 },
                    { 14, 101 },
                    { 14, 103 },
                    { 14, 106 },
                    { 15, 103 },
                    { 15, 105 },
                    { 15, 106 }
                });

            migrationBuilder.InsertData(
                table: "SimilarFilms",
                columns: new[] { "FilmId", "SimilarFilmId" },
                values: new object[,]
                {
                    { 11, 12 },
                    { 11, 13 },
                    { 11, 14 },
                    { 12, 11 },
                    { 12, 13 },
                    { 12, 14 },
                    { 13, 11 },
                    { 13, 14 },
                    { 13, 15 },
                    { 14, 11 },
                    { 14, 12 },
                    { 14, 15 },
                    { 15, 12 },
                    { 15, 13 },
                    { 15, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenres_GenreId",
                table: "FilmGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_DirectorId",
                table: "Films",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarFilms_SimilarFilmId",
                table: "SimilarFilms",
                column: "SimilarFilmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmGenres");

            migrationBuilder.DropTable(
                name: "SimilarFilms");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
