using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Maurer.XUnit.Utilities.Integration
{
    /// <summary>
    /// Integration testing utility that bootstraps an instance of a web API under test.
    /// Modernized for hosting a 'Startup' application.
    /// </summary>
    /// <typeparam name="TStartup">Entry point type; requires Startup.cs.</typeparam>

    public class StartupHarness<TStartup> : ProgramHarness<TStartup>
        where TStartup : class
    {
        /// <summary>
        /// Default constructor facilitating the injection of a 'WebApplicationFactory'
        /// </summary>
        /// <param name="factory">Factory for boostrapping an application in memory for functional end to end tests</param>

        public StartupHarness(WebApplicationFactory<TStartup> factory) : base(factory)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.UseStartup<TStartup>();
        }
    }
}
