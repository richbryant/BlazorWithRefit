using System;
using System.Threading.Tasks;
using BlazorWithRefit.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace BlazorWithRefit.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

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
