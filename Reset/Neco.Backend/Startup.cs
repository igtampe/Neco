using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Igtampe.Neco.Data;

namespace Igtampe.Neco.Backend {
    public class Startup {

        /// <summary>Allow SpecificOrigins for CORS</summary>
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder => {
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowAnyOrigin();
                    });
            });

            services.AddControllers();
            services.AddDbContext<NecoContext>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Neco.Backend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            app.UseCors(MyAllowSpecificOrigins);
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Neco.Backend v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
