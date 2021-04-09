using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServicesStore.Api.CartService.Application;
using ServicesStore.Api.CartService.Persistence;
using ServicesStore.Api.CartService.RemoteInterfaces;
using ServicesStore.Api.CartService.RemoteServices;

namespace ServicesStore.Api.CartService
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
            //Important to add dependency services before!
            services.AddScoped<IBooksService, BooksService>();

            services
                .AddControllers()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Create>());
            ;
            services.AddDbContext<CartContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DbConnection"));
            });

            services.AddMediatR(typeof(Create.Handler).Assembly);

            //dtos
            services.AddAutoMapper(typeof(Create.Handler));

            //http
            services.AddHttpClient("Books",(cfg)=>
            {
                cfg.BaseAddress = new System.Uri(Configuration["Services:Books"]);
            });
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
