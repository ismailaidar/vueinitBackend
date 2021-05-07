using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using patients.Services;
using VueCliMiddleware;

namespace patients
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
            services.AddDbContext<PatientDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("PatientDbContext")));


            services.AddSpaStaticFiles(configuration: options => { options.RootPath = "vueInit/dist"; });
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("https://localhost:5001");
                });
            });
            
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "patients", Version = "v1" });
            });

            // DI
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

            // In production, the SPA files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "vueInit/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "patients v1"));
            }

            app.UseCors("VueCorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMvc();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSpaStaticFiles();

            app.UseSpa(configuration: builder =>
            {
                builder.Options.SourcePath = "vueInit";
                if (env.IsDevelopment())
                {
                    builder.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });

            if (System.Diagnostics.Debugger.IsAttached)
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();

                    // NOTE: VueCliProxy is meant for developement and hot module reload
                    // NOTE: SSR has not been tested
                    // Production systems should only need the UseSpaStaticFiles() (above)
                    // You could wrap this proxy in either
                    // if (System.Diagnostics.Debugger.IsAttached)
                    // or a preprocessor such as #if DEBUG
                    endpoints.MapToVueCliProxy(
                        "{*path}",
                        new SpaOptions { SourcePath = "vueInit" },
                        npmScript: (System.Diagnostics.Debugger.IsAttached) ? "serve" : null,
                        regex: "Compiled successfully",
                        forceKill: true
                        );
                });
            }

        }
    }
}
