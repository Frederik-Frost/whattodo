using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using whattodo.Content;


namespace whattodo;

[Route("[controller]")]
public class DataController : ControllerBase
{

    [HttpGet]
    public async Task<List<CustomDataFormat>> GetAreaData(string id)
    {
        Address Address = new Address();
        CityData cityData = await Address.GetCityFromPostalCode(id);

        WeatherForecast WeatherForecast = new WeatherForecast();
        WeatherForecastResult forecast = await WeatherForecast.GetForecast(cityData.Visueltcenter[0], cityData.Visueltcenter[1]);

        var variableStore = new List<CustomDataFormat>{
            // City
            new CustomDataFormat("By", "Bynavn", cityData.Navn, VariableType.Text),
            new CustomDataFormat("Postnummer", "Byens postnummer", cityData.Nr, VariableType.Text),
            new CustomDataFormat("Visuelt center", "Byens koordinater", cityData.Visueltcenter, VariableType.List),
            new CustomDataFormat("Visuelt center", "Byens koordinater", cityData.Visueltcenter, VariableType.List),

            // Weather
            new CustomDataFormat("Meta_Updated at", "Last updated metadata", forecast.Properties.Meta.UpdatedAt, VariableType.Date),
            new CustomDataFormat("Meta_Units_Air temperature", "Temperature unit", forecast.Properties.Meta.Units.AirTemperature, VariableType.Text),
            new CustomDataFormat("Meta_Units_Precipitation amount", "The unit of the amount of rain, snow, hail, etc., that has fallen at a given place within a given period", forecast.Properties.Meta.Units.PrecipitationAmount, VariableType.Text),
            new CustomDataFormat("Meta_Units_Relative humidity", "The unit of humidity", forecast.Properties.Meta.Units.RelativeHumidity, VariableType.Text),
            new CustomDataFormat("Meta_Units_Wind direction", "The unit of the wind direction", forecast.Properties.Meta.Units.WindFromDirection, VariableType.Text),
            new CustomDataFormat("Meta_Units_Wind speed", "The unit of the wind speed", forecast.Properties.Meta.Units.WindSpeed, VariableType.Text),

            new CustomDataFormat("Meta_Units_Wind speed", "The unit of the wind speed", forecast.Properties.Meta.Units.WindSpeed, VariableType.Text),

            





        };

        return variableStore;

    }

}
