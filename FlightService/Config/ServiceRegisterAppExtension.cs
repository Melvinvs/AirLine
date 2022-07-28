using Consul;
using IApplicationLifetime = Microsoft.Extensions.Hosting.IApplicationLifetime;

namespace FlightService.Config
{
    public static class ServiceRegisterAppExtension
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services)
        {
            string ConsulAddress = "http://localhost:8500/";
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(c =>
            {
                c.Address = new Uri(ConsulAddress);
            }));

            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");

            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            var registration = new AgentServiceRegistration()
            {
                ID = "FlightService1",
                Name = "FlightService",
                Address = "localhost",
                Port = 7038
            };

            logger.LogInformation("Registering consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(async () =>
            {
                logger.LogInformation("Unregistering");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            return app;
        }
    }
}
