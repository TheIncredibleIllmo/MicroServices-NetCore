using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServicesStore.Api.Author.Application;
using ServicesStore.Api.Author.Persistence;

namespace ServicesStore.Api.Author
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
            services
                .AddControllers()
                //body validation
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Create>());

            services.AddDbContext<BookAuthorContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DbConnection"));
            });

            //handlers
            services.AddMediatR(typeof(Create.Handler).Assembly);

            //dtos
            services.AddAutoMapper(typeof(GetAll.Handler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
