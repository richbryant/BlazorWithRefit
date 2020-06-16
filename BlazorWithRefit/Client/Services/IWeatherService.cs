using System.Threading.Tasks;
using BlazorWithRefit.Shared;
using Refit;

namespace BlazorWithRefit.Client.Services
{
    public interface IWeatherService
    {
        [Get("/WeatherForecast")]
        Task<WeatherForecast[]> GetForecasts();
    }
}