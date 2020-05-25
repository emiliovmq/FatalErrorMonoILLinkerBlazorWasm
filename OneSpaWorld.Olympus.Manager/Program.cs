// Copyright (c) OneSpaWorld (OSW). All rights reserved.

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneSpaWorld.Olympus.Manager
{
    /// <summary>
    /// the main program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// the main program entry point
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            // create the builder
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // set the root component
            builder.RootComponents.Add<App>("app");

            // configuring the logger
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

            

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                builder.Configuration.Bind("OpenIdConnect", options.ProviderOptions);
            });

            // other services
            //builder.Services.AddHeadElementHelper();

            // build & run
            await builder.Build().RunAsync().ConfigureAwait(false);
        }
    }
}
