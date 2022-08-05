using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ToDo.Data;
using ToDo.Data.Repositorys;
using ToDo.Models.Interfaces;
using ToDo.WebApi.Authentication;
using ToDo.WebApi.Mapping;
using ToDo.WebApi.Services;
using ToDo.WebApi.Settings;

namespace ToDo.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectinStrings = new ApplicationSettings(Configuration);
            services.AddSingleton<ApplicationSettings>();

            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256,

                    }
                );

            services.AddScoped<ISecureService, SecureService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(AppMappingProfile));

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(connectinStrings.DbConnectionString);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            services.AddAuthentication(MyCustomTokenAuthOptions.DefaultScemeName)
                .AddScheme<MyCustomTokenAuthOptions, MyCustomTokenAuthHandler>(
                    MyCustomTokenAuthOptions.DefaultScemeName,
                    opts =>
                    {
                    }
            );

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<INoteService, NoteService>();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ToDoWebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.OperationFilter<AddAuthorizationHeaderOperationHeader>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoWebApi V1");
                c.RoutePrefix = "api";
                c.DisplayRequestDuration();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
