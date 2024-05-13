
using System.Text;
using AppCore.IRepositories;
using Infrastracture.Db;
using Infrastracture.Repositoreis;
using Infrastracture.Service;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BookstoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json");

            // Configure logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Information);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add DbContext with PostgreSQL provider
            builder.Services.AddDbContext<gravity_booksContext>(options =>
                options.UseNpgsql(connectionString));

            //Automapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers();

            //Service
            builder.Services.AddScoped<IBookService, BookService>();

            //Repository
            builder.Services.AddScoped<IBookRepositories, BookRepositories>();

            //Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

                options.MapType<Stream>(() => new OpenApiSchema { Type = "file", Format = "binary" });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();

                //Add access for swagger model example
                options.ExampleFilters();

                options.CustomSchemaIds(type =>
                {
                    return type.Name switch
                    {
                        _ => type.Name
                    };
                });
            });


            builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

            // Configure JWT authentication for the application.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration.GetSection("AppSettings:Token").Value!))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Headers["Authorization"].FirstOrDefault();

                        if (!string.IsNullOrEmpty(token))
                        {
                            if (token.StartsWith("Bearer "))
                            {
                                token = token.Substring("Bearer ".Length);
                            }
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Book Api V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
