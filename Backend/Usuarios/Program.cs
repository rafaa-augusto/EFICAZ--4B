using Application.Services;
using Core.Repositories;
using Core.Services;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Usuarios
{
    public class Program
    {
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cadastro Usuario API", Version = "v1" });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private static void InjectRepositoryDependency(IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<CadastroUsuariosDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );
        }

        private static void AddControllersAndDependencies(IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEnderecoService, EnderecoService>();
            builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();


        }

        private static void ConfigureCors(IHostApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirTodasOrigens", builder =>
                {
                    builder.AllowAnyOrigin() // Permite qualquer origem
                           .AllowAnyMethod() // Permite qualquer método (GET, POST, etc.)
                           .AllowAnyHeader(); // Permite qualquer cabeçalho
                });
            });
        }

        private static void Authentication(IHostApplicationBuilder builder)
        {
            // Configuração de autenticação e autorização
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SECRET_KEY"]!))
                    };
                });
            builder.Services.AddAuthorization();
        }

        private static void InitializeSwagger(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        /*private static void SeedOnInitialize(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<CadastroUsuariosDbContext>();
                SeedDate.Initialize(services);
            }
            catch (Exception ex)
            {
                // Log de erro
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Ocorreu um erro ao inicializar o banco de dados com dados de seed.");
            }
        }*/

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Authentication(builder);
            ConfigureSwagger(builder.Services);
            InjectRepositoryDependency(builder);
            AddControllersAndDependencies(builder);
            ConfigureCors(builder);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                InitializeSwagger(app);
            }

            //SeedOnInitialize(app);


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.UseCors("PermitirTodasOrigens");
            app.Run();
        }
    }
}
