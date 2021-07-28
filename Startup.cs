using dotnetServer.Application.Middlewares;
using dotnetServer.Domain.Respositories;
using dotnetServer.Services.ProfileServices;
using dotnetServer.Infra.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using dotnetServer.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using dotnetServer.Services.AuthorizationServices;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace dotnetServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetServer", Version = "v1" });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                    {
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                        Name = "JWT Authentication",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, System.Array.Empty<string>() }
                });
            });

            services.AddSingleton<ConfigService>();
            
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(Configuration.GetValue<string>("postgres_connection_string")));

            services.AddScoped<DataContext>();

            #region Authorization Services
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>(ConfigService.JwtSecreteFieldName));
            services.AddAuthentication(configureOptions =>
            {	
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            services.AddSingleton<TokenService>();
            services.AddSingleton<EncryptionService>();
            #endregion

            #region Profile Services
            services.AddScoped<IProfileRepository, PostgresProfileRepository>();
            services.AddScoped<CreateProfileService>();
            #endregion

            // services.AddAuthentication
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnetServer v1"));
            }

            app.UseMiddleware<ExceptionMeddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
