using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Maurer.XUnit.Utilities.Integration
{
    /// <summary>
    /// Integration testing utility that bootstraps an instance of a web API under test.
    /// Modernized for minimal hosting (Program.cs). Still works with Startup via override.
    /// </summary>
    /// <typeparam name="TApplication">Entry point type; requires at minimum public class Program.</typeparam>

    public class ProgramHarness<TApplication> : IClassFixture<WebApplicationFactory<TApplication>>
        where TApplication : class
    {
        /// <summary>
        /// Factory for boostrapping an application in memory for functional end to end tests
        /// </summary>

        private readonly WebApplicationFactory<TApplication> _factory;

        /// <summary>
        /// Configures the web host builder to use the environment and appconfiguration specified in Settings.cs
        /// </summary>
        /// <param name="builder">Instance of IWebHostBuilder</param>

        virtual protected void ConfigureWebHost(IWebHostBuilder builder)
        {
            var environment = string.IsNullOrWhiteSpace(Settings.Environment)
                ? Environments.Development
                : Settings.Environment;

            builder.UseEnvironment(environment);
            builder.ConfigureAppConfiguration((context, configuration) =>
            {
                var env = context.HostingEnvironment;

                // 1) Pick a base config directory:
                //    CONFIG_DIR env var > <contentRoot>/config (if it exists) > <contentRoot>
                string configurationDirectory =
                    Environment.GetEnvironmentVariable("CONFIG_DIR")
                    ?? (Directory.Exists(Path.Combine(env.ContentRootPath, "config"))
                            ? Path.Combine(env.ContentRootPath, "config")
                            : env.ContentRootPath);

                // 2) If a file name is specified (e.g., "appsettings.json"), load it.
                if (!string.IsNullOrWhiteSpace(Settings.AppConfiguration))
                {
                    // Respect absolute paths; otherwise look under configurationDirectory
                    var configurationPath = Path.IsPathRooted(Settings.AppConfiguration)
                        ? Settings.AppConfiguration
                        : Path.Combine(configurationDirectory, Settings.AppConfiguration);

                    configuration.AddJsonFile(configurationPath, optional: true, reloadOnChange: true);
                }

                // 3) Support key-per-file (e.g., Kubernetes ConfigMap/Secret mounted as files).
                var keyPerFileDir =
                    Environment.GetEnvironmentVariable("CONFIG_KEYS_DIR") // allow override
                    ?? Path.Combine(env.ContentRootPath, "config-keys");

                if (Directory.Exists(keyPerFileDir))
                {
                    // Requires the package: Microsoft.Extensions.Configuration.KeyPerFile (8.x/9.x)
                    configuration.AddKeyPerFile(directoryPath: keyPerFileDir, optional: true, reloadOnChange: true);
                }

                // Optional: environment variables can override everything:
                // config.AddEnvironmentVariables(prefix: "APP_");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>

        virtual protected void ConfigureServices(IServiceCollection services) 
        {

        }

        /// <summary>
        /// Initial 'act' boostrapping a client and server for end to end testing
        /// </summary>
        /// <param name="parameters">Set of arguments to act on (unused by default)</param>

        virtual protected void Act(params object[] parameters)
        {
            var factory = _factory.WithWebHostBuilder(ConfigureWebHost);

            Client = factory.CreateClient(ClientOptions);
            Server = factory.Server;
            Configuration = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IConfiguration>();
        }

        /// <summary>
        /// The default options to use when creating instances of HttpClient using WebApplicationFactory.CreateClient (Disallows redirects by default; can be overridden).
        /// </summary>

        protected virtual WebApplicationFactoryClientOptions ClientOptions => new()
        {
            AllowAutoRedirect = false,
            MaxAutomaticRedirections = 0
        };

        /// <summary>
        /// Default constructor facilitating the injection of a 'WebApplicationFactory'
        /// </summary>
        /// <param name="factory">Factory for boostrapping an application in memory for functional end to end tests</param>

        public ProgramHarness(WebApplicationFactory<TApplication> factory) 
            => _factory = factory;

        /// <summary>
        /// Represents a set of key/value application configuration properties
        /// </summary>

        public IConfiguration Configuration { get; private set; } = default!;

        /// <summary>
        /// Microsoft.AspNetCore.Hosting.Server.IServer implementation for executing tests
        /// </summary>

        public TestServer Server { get; private set; } = default!;

        /// <summary>
        /// Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI
        /// </summary>

        public HttpClient Client { get; private set; } = default!;
    }
}