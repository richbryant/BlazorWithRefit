# BlazorWithRefit
Very simple demo of Blazor on wasm with [Refit](https://github.com/reactiveui/refit), [Polly](http://www.thepollyproject.org/) and System.Text.Json

## Short Form - 

Take a look in Program.cs, it contains pretty much everything you need.  The Polly stuff is marked with a comment, feel free to remove it if you don't want Polly.

## Longer version

This is an AspNetCore Hosted basic template, all I've done is swap out the direct call to WeatherForecastController with a service that uses Refit.

As you can see, it's a straightforward Refit service marked with a `[Get]` attribute.

```
    public interface IWeatherService
    {
        [Get("/WeatherForecast")]
        Task<WeatherForecast[]> GetForecasts();
    }
```

Naturally, that's injected into FetchData.razor instead of the usual HttpClient.

All the setup for this is in `program.cs` as mentioned above, but in order to use System.Text.Json - which is the new default for AspNetCore - instead of Refit's preferred NewtonSoft.Json, you also need to override the default serializer.  This is really simple.  Just copy mine.  [You'll find it here](https://github.com/richbryant/BlazorWithRefit/blob/main/BlazorWithRefit/Client/JsonContentSerializer.cs).  The setup for this is in the `settings` variable in program.cs. I could have inlined it but it's clearer this way.

Take what you need, go, enjoy cleaner RESTful Api calls from Blazor on wasm!


