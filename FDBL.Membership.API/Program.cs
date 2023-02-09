using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;

namespace FDBL.Membership.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices();
        ConfigureAutoMapper();

        // Configure the HTTP request pipeline.
        ConfigureMiddleware();

        void ConfigureMiddleware()
        {
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsAllAccessPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        void ConfigureServices()
        {
            builder.Services.AddCors(policy => {
                policy.AddPolicy("CorsAllAccessPolicy", opt =>
                opt.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                 );
            });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FDBLContext>(
            options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("FDBLConnection")));

            //Register a scoped service for IDbService and DbService
            builder.Services.AddScoped<IDbService, DbService>();
        }

        //To convert between entities and DTOs, you must configure mappings between them for AutoMapper in the services section
        // Configuration Methods
        void ConfigureAutoMapper()
        {
            /* a configuration object for AutoMapper, which registers 
            the mappings between entities and DTOs. The CreateMap method adds a mapping 
            from an entity to a DTO, and the ReverseMap method ads a mapping from DTO to 
            Entity.*/
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Genre, GenreDTO>().ReverseMap();

                cfg.CreateMap<Director, DirectorDTO>().ReverseMap()
                .ForMember(dest => dest.Films, src => src.Ignore());

                cfg.CreateMap<Film, FilmDTO>().ReverseMap()
                .ForMember(dest => dest.Director, src => src.Ignore());

                cfg.CreateMap<SimilarFilm, SimilarFilmDTO>().ReverseMap();

                cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();

                cfg.CreateMap<FilmCreateDTO, Film>().ReverseMap();
                cfg.CreateMap<FilmEditDTO, Film>().ReverseMap();
            });

            var mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);
        }
    }
}