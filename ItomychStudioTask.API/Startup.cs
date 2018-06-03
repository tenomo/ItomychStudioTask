using AutoMapper;
using ItomychStudioTask.Business.Services.Books;
using ItomychStudioTask.Business.Services.Categories;
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.SqlLite;
using ItomychStudioTask.Data.SqlLite.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;


namespace ItomychStudioTask.API
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
            services.AddMvc();
            services.AddCors();
            services.AddDbContext<IStorageContext, StorageContext>(options =>
                   options.UseSqlite("Data Source=ItomychStudioTaskDB.db"));

            services.AddScoped(typeof(IStorageContext), typeof(StorageContext));
            services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IStorage), typeof(Storage));
            services.AddScoped(typeof(IBookValidationService), typeof(BookValidationService));
            services.AddScoped(typeof(IBookService), typeof(BookService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
             services.AddAutoMapper();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",new Info{Title = "ItomychStudio Task: Books REST Service", Description= "A REST service that manages books collections." });
                var xmlDocPath = $"{System.AppDomain.CurrentDomain.BaseDirectory}/Documentation.xml";
                options.IncludeXmlComments(xmlDocPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "ItomychStudio Task: Books REST Service");
            });
        }
    }
}
