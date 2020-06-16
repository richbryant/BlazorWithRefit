using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using BlazorWithRefit.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;

namespace BlazorWithRefit.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var settings = new RefitSettings
            {
                ContentSerializer = new JsonContentSerializer()
            };

            builder.Services.AddRefitClient<IWeatherService>(settings).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://localhost:44366/api");
            });

            await builder.Build().RunAsync();
        }
    }
}
