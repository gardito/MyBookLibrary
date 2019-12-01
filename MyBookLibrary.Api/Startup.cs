using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyBookLibrary.Core;
using MyBookLibrary.Core.Services;
using MyBookLibrary.Data;
using MyBookLibrary.Services;

namespace MyBookLibrary.Api
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<MyBookLibraryDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("Default"),
                x => x.MigrationsAssembly("MyBookLibrary.Data")));
            
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Book Library",
                    Version = "v1" })
            );

            services.AddAutoMapper(typeof(Startup));
        }

        public void ApplyMigrations(MyBookLibraryDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Book Library V1");
            });
        }
    }
}
