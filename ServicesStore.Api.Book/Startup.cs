using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServicesStore.Api.Book.Application;
using ServicesStore.Api.BookService.Persistence;
using ServiceStore.RabbitMQ.Bus.RabbitBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book
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
            //services.AddTransient<IRabbitEventBus, RabbitEventBus>();

            services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var mediatorService = sp.GetService<IMediator>();
                return new RabbitEventBus(mediatorService, scopeFactory);
            });

            services
                .AddControllers()
                .AddFluentValidation(cfg=>cfg.RegisterValidatorsFromAssemblyContaining<Create>());

            services.AddDbContext<LibraryMaterialContext>(opt => 
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
 
            });

            services.AddMediatR(typeof(Create.Handler).Assembly);

            //dtos
            services.AddAutoMapper(typeof(Create.Handler));
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
