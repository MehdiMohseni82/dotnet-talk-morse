using DotNetTalk.MorseMessenger.Api.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetTalk.MorseMessenger.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string _allowedSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: _allowedSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://morse-message.dotnet-talk.com/",
                                            "https://morse-message.dotnet-talk.com/");
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messenger UI");
            });


            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MorseHub>("/morse-hub");
                endpoints.MapControllers();
            });
        }
    }
}
