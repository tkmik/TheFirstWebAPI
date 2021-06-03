using Autofac;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TheFirstWebAPI.Models;
using TheFirstWebAPI.Services;
using WebAPILibrary;
using WebAPILibrary.Services;

namespace TheFirstWebAPI
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
            //NewtonsoftJSON
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });
            services.AddDbContext<AppDbContext>();
            //services.AddTransient<IUserService, UserService>();

            //FluentValidation
            services.AddFluentValidation();
            //services.AddTransient<IValidator<UserDTO>, UserValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheFirstWebAPI", Version = "v1" });
            });

            //AutoMapper
            services.AddAutoMapper(map => map.AddProfile<AutoMapping>(), typeof(Startup));
            //services.AddAutoMapper(typeof(Startup));
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Autofac
            //add fluent validation
            builder.RegisterType<UserValidator>().As<IValidator<UserDTO>>();
            builder.RegisterType<UserService>().As<IUserService>();

            //builder.RegisterType<UserService>().As<IUserService>();
            //builder.RegisterType<SomeType>().As<ISomeType>();
            //ApplicationContainer = builder.Build();
            //var container = builder.Build();
            //return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheFirstWebAPI v1"));
            }

            //AutofacContainer = app.ApplicationServices.GetAutofacRoot();            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
