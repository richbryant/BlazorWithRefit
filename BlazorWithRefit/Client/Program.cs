using System;
using System.Threading.Tasks;
using BlazorWithRefit.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Polly;
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
            })
                //Retry policy using Polly
                //You could also add a fallback policy, a circuit-breaker or any combination of these
                .AddTransientHttpErrorPolicy(b => b.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }));

            await builder.Build().RunAsync();
        }
    }
}
