namespace FDBL.Token.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        RegisterServices(builder);

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

        void RegisterServices(WebApplicationBuilder builder)
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

            builder.Services.AddDbContext<FDBLUserContext>(
                options =>
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("FDBLUserConnection")));

            builder.Services.AddIdentity<FDBLUser, IdentityRole>()
                    .AddEntityFrameworkStores<FDBLUserContext>()
                    .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

        }
    }
}