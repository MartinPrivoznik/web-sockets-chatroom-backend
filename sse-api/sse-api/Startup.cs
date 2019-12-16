using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.AspNetCore.Hosting;
using sse_api.Services.sse_extensions;
using sse_api.Core.Services.sse_services;
using sse_api.Services.sse_services;
using sse_api.Services;
using Newtonsoft.Json;

namespace sse_api
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
            services.AddCors();

            services.AddServerSentEvents();
            services.AddServerSentEvents<INotificationsServerSentEventsService, NotificationsServerSentEventsService>();
            services.AddServerSentEvents<IChatroomServerSentEventsService, ChatroomServerSentEventsService>();

            services.AddSingleton<IHostedService, HeartbeatService>();
            services.AddNotificationsService(Configuration);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "text/event-stream" });
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); //For large jsons

                services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthorization();

            app.UseResponseCompression()
                .UseRouting()
                .UseEndpoints(endpoints =>
            {
                // Set up first Server-Sent Events endpoint.
                endpoints.MapServerSentEvents("/sse-heartbeat");

                // Set up second (separated) Server-Sent Events endpoint.
                endpoints.MapServerSentEvents<NotificationsServerSentEventsService>("/sse-notifications");

                endpoints.MapServerSentEvents<ChatroomServerSentEventsService>("/sse-chatroom");

                endpoints.MapControllers();
            });
        }
    }
}
