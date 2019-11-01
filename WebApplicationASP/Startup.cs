using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplicationASP.Models;
using WebApplicationASP_2.Services;

namespace WebApplicationASP_2
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers();
            services.AddTransient<JsonFileProductService>();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                //products.json ligger i /data. Innehåller object. JsonFileProductService.cs skapar en string för filename. Hämtar infon i products.json.
                // /products = html-sidan. context = metod vi kör. context definieras innanför {}. 
                // products är en IEnumerable av <Product> som heter products.
                // json är en string som skapas från products.
                //
                endpoints.MapGet("/products", (context) =>
                {
                    var products = app.ApplicationServices.GetService<JsonFileProductService>().GetProducts();
                    var json = JsonSerializer.Serialize<IEnumerable<Student>>(products);
                    return context.Response.WriteAsync(json);
                });
            });
        }
    }
}
