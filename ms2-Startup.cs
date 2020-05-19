using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using card_validate_ms.Consumer;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using rabbitmq;

namespace card_validate_ms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public object ContractsConstants { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<OrderCardNumberValidateConsumer>();

                cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider, (cfg, host) =>
                {
                    cfg.ReceiveEndpoint(BusConstants.OrderQueue, ep =>
                    {
                        ep.ConfigureConsumer<OrderCardNumberValidateConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
