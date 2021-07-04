using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;

namespace SignalR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IHubContext<WeatherForecastHub> _hub;

        public WeatherForecastController(IHubContext<WeatherForecastHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", "Get Forecast", "successful");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
