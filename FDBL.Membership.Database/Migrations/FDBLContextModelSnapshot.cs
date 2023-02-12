﻿// <auto-generated />
using System;
using FDBL.Membership.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FDBL.Membership.Database.Migrations
{
    [DbContext(typeof(FDBLContext))]
    partial class FDBLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FDBL.Membership.Database.Entities.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Directors");

                    b.HasData(
                        new
                        {
                            Id = 1001,
                            Name = "Brad Peyton"
                        },
                        new
                        {
                            Id = 1002,
                            Name = "Jordan Vogt-Roberts"
                        },
                        new
                        {
                            Id = 1003,
                            Name = "Brad Peyton"
                        });
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<string>("FilmUrl")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<bool>("Free")
                        .HasColumnType("bit");

                    b.Property<string>("ImageLargeUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImageSimilarUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Released")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("Films");

                    b.HasData(
                        new
                        {
                            Id = 11,
                            Description = "När tre olika djur smittas av en farlig patogen som gör dem till jättemonster slår sig en primatolog ihop med en genetiker för att hindra dem från att ödelägga Chicago.",
                            DirectorId = 1001,
                            FilmUrl = "https://youtu.be/coOKvrsmQiI",
                            Free = false,
                            ImageLargeUrl = "/images/Rampage_large.png",
                            ImageSimilarUrl = "/images/Rampage_sim.png",
                            ImageUrl = "/images/Rampage.png",
                            Released = new DateTime(2018, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Rampage"
                        },
                        new
                        {
                            Id = 12,
                            Description = "När den okända San Andreasförkastningen slutligen ger vika, vilket utlöser en jordbävning av magnitud 9 i Kalifornien, tar sig en helikopterräddare och hans förfrämligade fru från Los Angeles till San Francisco för att rädda sin enda dotter.",
                            DirectorId = 1001,
                            FilmUrl = "https://youtu.be/23VflsU3kZE",
                            Free = false,
                            ImageLargeUrl = "/images/SanAndreas_large.png",
                            ImageSimilarUrl = "/images/SanAndreas_sim.png",
                            ImageUrl = "/images/SanAndreas.png",
                            Released = new DateTime(2015, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "San Andreas"
                        },
                        new
                        {
                            Id = 13,
                            Description = "En brokig skara forskare, soldater och äventyrare samlas för att utforska en vacker men förrädisk ö i Stilla havet, omedvetna om att de inkräktar på den mytiska Kongs territorium.",
                            DirectorId = 1002,
                            FilmUrl = "https://youtu.be/dBLdPIp-BuY",
                            Free = false,
                            ImageLargeUrl = "/images/KonSkullIsland_large.png",
                            ImageSimilarUrl = "/images/KonSkullIsland_sim.png",
                            ImageUrl = "/images/KonSkullIsland.png",
                            Released = new DateTime(2017, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Kong Skull Island"
                        },
                        new
                        {
                            Id = 14,
                            Description = "Den unge Sean Anderson får ett kodat nödanrop från en otrolig plats: en outforskad mystisk ö. Så inleds äventyret i uppföljaren till succén ”Journey to the Center of the Earth”.",
                            DirectorId = 1001,
                            FilmUrl = "https://youtu.be/1Q2LVXlHKS8",
                            Free = false,
                            ImageLargeUrl = "/images/Journey2_large.png",
                            ImageSimilarUrl = "/images/Journey2_sim.png",
                            ImageUrl = "/images/Journey2.png",
                            Released = new DateTime(2012, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Journey2"
                        },
                        new
                        {
                            Id = 15,
                            Description = "Maltazard har fångat Arthur i Minimojernas rike och har påbörjat kampen för att ta över människornas värld.",
                            DirectorId = 1003,
                            FilmUrl = "https://youtu.be/QWHsbDbLCJM",
                            Free = false,
                            ImageLargeUrl = "/images/Arthur_large.png",
                            ImageSimilarUrl = "/images/Arthur_sim.png",
                            ImageUrl = "/images/Arthur.png",
                            Released = new DateTime(2011, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Arthur The War Of Two Worlds"
                        });
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.FilmGenre", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("FilmGenres", (string)null);

                    b.HasData(
                        new
                        {
                            FilmId = 11,
                            GenreId = 101
                        },
                        new
                        {
                            FilmId = 11,
                            GenreId = 102
                        },
                        new
                        {
                            FilmId = 11,
                            GenreId = 103
                        },
                        new
                        {
                            FilmId = 12,
                            GenreId = 101
                        },
                        new
                        {
                            FilmId = 12,
                            GenreId = 103
                        },
                        new
                        {
                            FilmId = 12,
                            GenreId = 104
                        },
                        new
                        {
                            FilmId = 13,
                            GenreId = 101
                        },
                        new
                        {
                            FilmId = 13,
                            GenreId = 103
                        },
                        new
                        {
                            FilmId = 13,
                            GenreId = 105
                        },
                        new
                        {
                            FilmId = 14,
                            GenreId = 101
                        },
                        new
                        {
                            FilmId = 14,
                            GenreId = 103
                        },
                        new
                        {
                            FilmId = 14,
                            GenreId = 106
                        },
                        new
                        {
                            FilmId = 15,
                            GenreId = 103
                        },
                        new
                        {
                            FilmId = 15,
                            GenreId = 105
                        },
                        new
                        {
                            FilmId = 15,
                            GenreId = 106
                        });
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 101,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 102,
                            Name = "Sci-Fi"
                        },
                        new
                        {
                            Id = 103,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 104,
                            Name = "Thriller"
                        },
                        new
                        {
                            Id = 105,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 106,
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = 107,
                            Name = "Animation"
                        },
                        new
                        {
                            Id = 108,
                            Name = "Family"
                        });
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.SimilarFilm", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("SimilarFilmId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "SimilarFilmId");

                    b.HasIndex("SimilarFilmId");

                    b.ToTable("SimilarFilms");

                    b.HasData(
                        new
                        {
                            FilmId = 11,
                            SimilarFilmId = 12
                        },
                        new
                        {
                            FilmId = 11,
                            SimilarFilmId = 13
                        },
                        new
                        {
                            FilmId = 11,
                            SimilarFilmId = 14
                        },
                        new
                        {
                            FilmId = 12,
                            SimilarFilmId = 11
                        },
                        new
                        {
                            FilmId = 12,
                            SimilarFilmId = 14
                        },
                        new
                        {
                            FilmId = 12,
                            SimilarFilmId = 13
                        },
                        new
                        {
                            FilmId = 13,
                            SimilarFilmId = 11
                        },
                        new
                        {
                            FilmId = 13,
                            SimilarFilmId = 15
                        },
                        new
                        {
                            FilmId = 13,
                            SimilarFilmId = 14
                        },
                        new
                        {
                            FilmId = 14,
                            SimilarFilmId = 12
                        },
                        new
                        {
                            FilmId = 14,
                            SimilarFilmId = 11
                        },
                        new
                        {
                            FilmId = 14,
                            SimilarFilmId = 15
                        },
                        new
                        {
                            FilmId = 15,
                            SimilarFilmId = 12
                        },
                        new
                        {
                            FilmId = 15,
                            SimilarFilmId = 13
                        },
                        new
                        {
                            FilmId = 15,
                            SimilarFilmId = 14
                        });
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.Film", b =>
                {
                    b.HasOne("FDBL.Membership.Database.Entities.Director", "Director")
                        .WithMany("Films")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.FilmGenre", b =>
                {
                    b.HasOne("FDBL.Membership.Database.Entities.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FDBL.Membership.Database.Entities.Genre", "Genres")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Genres");
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.SimilarFilm", b =>
                {
                    b.HasOne("FDBL.Membership.Database.Entities.Film", "Film")
                        .WithMany("SimilarFilms")
                        .HasForeignKey("FilmId")
                        .IsRequired();

                    b.HasOne("FDBL.Membership.Database.Entities.Film", "Similar")
                        .WithMany()
                        .HasForeignKey("SimilarFilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Similar");
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.Director", b =>
                {
                    b.Navigation("Films");
                });

            modelBuilder.Entity("FDBL.Membership.Database.Entities.Film", b =>
                {
                    b.Navigation("SimilarFilms");
                });
#pragma warning restore 612, 618
        }
    }
}
