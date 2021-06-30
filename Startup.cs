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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetServer", Version = "v1" });
            });

            // services.AddMvcCore().AddJsonOptions(c => c.JsonSerializerOptions.)
            //  AddJsonFormatters(j => j.ContractResolver = new CamelCasePropertyNamesContractResolver());
            
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(Configuration.GetValue<string>("postgres_connection_string")));

            services.AddScoped<DataContext>();

            #region Profile Services
            services.AddScoped<IProfileRepository, PostgresProfileRepository>();
            services.AddScoped<CreateProfileService>();
            #endregion
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
