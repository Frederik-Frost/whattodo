using Microsoft.AspNetCore.Mvc;
using whattodo.Content;
namespace whattodo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public Task<WeatherForecastResult> GetAreaForecast(float lat, float lon)
    {
        // lat == visueltcenter_y && lon == visueltcenter_x
        WeatherForecast WeatherForecast = new WeatherForecast();
        var res = WeatherForecast.GetForecast(lat, lon);
        
        return res;
    }   
}

