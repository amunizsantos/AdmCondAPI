using AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace AlexandreMMuniz.AdmCond.API
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
            EncryptionAES encryption = new EncryptionAES();

            // Desabilite esta linha somente para criptografar uma string de conexão e obter o resultado em modo debug.
            // Deverá estar sempre comentada em situação normal.
            //string encryptedConString = encryption.Encrypt("Data Source=DESKTOP-T28UCSE\\SQLEXPRESS;Initial Catalog=AlexandreMMunizAdmCondProd;user=sa;pwd=1234");

            services.AddControllers();

            services.AddDbContext<AlexandreMMunizAdmCondContext>(options =>
            {
                options.UseSqlServer(encryption.Decrypt(Configuration.GetConnectionString("AlexandreMMunizAdmCondDatabase")));
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AlexandreMMuniz Administração de Condomínios API",
                    Version = "v1",
                    Description = File.ReadAllText("APIDescription.html")
                });

                // Set the comments path for the Swagger JSON and UI.
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfo[] supportedCultures = new CultureInfo[]
            {
                new CultureInfo("pt-BR")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "AlexandreMMuniz Administração de Condomínios API V1");

                if (!env.IsDevelopment())
                {
                    c.SupportedSubmitMethods(Array.Empty<SubmitMethod>());
                }

            });

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
