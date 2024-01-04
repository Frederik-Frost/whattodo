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
        WeatherForecastResult forecast = await WeatherForecast.GetForecast(cityData.Visueltcenter[1], cityData.Visueltcenter[0]);

        var variableStore = new List<CustomDataFormat>{
            // City
            new CustomDataFormat("By", "Bynavn", cityData.Navn, VariableType.Text),
            new CustomDataFormat("Postnummer", "Byens postnummer", cityData.Nr, VariableType.Text),
            new CustomDataFormat("Visuelt center", "Byens koordinater", cityData.Visueltcenter, VariableType.List),
            new CustomDataFormat("Visuelt center", "Byens koordinater", cityData.Visueltcenter, VariableType.List),

            // Weather meta
            new CustomDataFormat("Meta_Updated at", "Last updated metadata", forecast.Properties.Meta.UpdatedAt, VariableType.Date),
            new CustomDataFormat("Meta_Units_Air temperature", "Temperature unit", forecast.Properties.Meta.Units.AirTemperature, VariableType.Text),
            new CustomDataFormat("Meta_Units_Cloud area fraction", "Cloud area unit", forecast.Properties.Meta.Units.CloudAreaFraction, VariableType.Text),
            new CustomDataFormat("Meta_Units_Precipitation amount", "The unit of the amount of rain, snow, hail, etc., that has fallen at a given place within a given period", forecast.Properties.Meta.Units.PrecipitationAmount, VariableType.Text),
            new CustomDataFormat("Meta_Units_Relative humidity", "The unit of humidity", forecast.Properties.Meta.Units.RelativeHumidity, VariableType.Text),
            new CustomDataFormat("Meta_Units_Wind direction", "The unit of the wind direction", forecast.Properties.Meta.Units.WindFromDirection, VariableType.Text),
            new CustomDataFormat("Meta_Units_Wind speed", "The unit of the wind speed", forecast.Properties.Meta.Units.WindSpeed, VariableType.Text),

            };

        // Weather timeseries
        int i = 0;
        foreach (var timeInstance in forecast.Properties.Timeseries)
        {
            variableStore.AddRange(new List<CustomDataFormat>{
                new CustomDataFormat($"Timeseries_{i+1}_Time", "Time", timeInstance.Time, VariableType.Date),
                new CustomDataFormat($"Timeseries_{i+1}_Instant_Details_Air temperature", "Air temp", timeInstance.Data.Instant.Details.AirTemperature, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Instant_Details_Cloud area fraction", "Clouds", timeInstance.Data.Instant.Details.CloudAreaFraction, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Instant_Details_Relative humidity", "Relative humidity", timeInstance.Data.Instant.Details.RelativeHumidity, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Instant_Details_Wind direction", "Wind direction", timeInstance.Data.Instant.Details.WindFromDirection, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Instant_Details_Wind speed", "Wind speed", timeInstance.Data.Instant.Details.WindSpeed, VariableType.Number),

                new CustomDataFormat($"Timeseries_{i+1}_Next 1 hours_Summary_Symbol code", "Symbol code", timeInstance.Data.Next1Hour.Summary.SymbolCode, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Next 1 hours_Summary_PrecipitationAmount", "PrecipitationAmount", timeInstance.Data.Next1Hour.Details.PrecipitationAmount, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Next 6 hours_Summary_Symbol code", "Symbol code", timeInstance.Data.Next6Hours.Summary.SymbolCode, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Next 6 hours_Summary_PrecipitationAmount", "PrecipitationAmount", timeInstance.Data.Next6Hours.Details.PrecipitationAmount, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Next 12 hours_Summary_Symbol code", "Symbol code", timeInstance.Data.Next12Hours.Summary.SymbolCode, VariableType.Number),
                new CustomDataFormat($"Timeseries_{i+1}_Next 12 hours_Summary_PrecipitationAmount", "PrecipitationAmount", timeInstance.Data.Next12Hours.Details.PrecipitationAmount, VariableType.Number),
            });
            i++;
        }

        return variableStore;

    }

}
