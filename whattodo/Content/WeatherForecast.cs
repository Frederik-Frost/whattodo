using System;
using whattodo.Utilitites;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace whattodo.Content
{
	public class WeatherForecast
	{
		string BaseUrl = "https://api.met.no/weatherapi/locationforecast/2.0/";
		// compact?lat=-16.516667&lon=-68.166667&altitude=4150
		// public WeatherForecast()
		// {
		// }

		public async Task<WeatherForecastResult> GetForecast(float lat, float lon)
		{
			string endpoint = $"compact?lat={lat}&lon={lon}";
			HttpHelper client = new HttpHelper(BaseUrl);

			var res = await client.GetAsync(new Uri(BaseUrl + endpoint));
			var content = await res.Content.ReadAsStringAsync();

			var json = JsonSerializer.Deserialize<WeatherForecastResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return json;
		}
	}

	public class WeatherForecastResult
	{
		public string Type { get; set; }
		public Geometry Geometry { get; set; }
		public Properties Properties { get; set; }
	}

	public class Geometry
	{
		public string Type { get; set; }
		public float[] Coordinates { get; set; }
	}

	public class Properties
	{
		public Meta Meta { get; set; }
		public List<Timeseries> Timeseries { get; set; }
	}

	public class Meta
	{

		[JsonPropertyName("updated_at")]
		public DateTime UpdatedAt { get; set; }
		public Units Units { get; set; }
	}

	public class Units
	{
		[JsonPropertyName("air_pressure_at_sea_level")]
		public string AirPressureAtSeaLevel { get; set; }
		[JsonPropertyName("air_temperature")]
		public string AirTemperature { get; set; }
		[JsonPropertyName("cloud_area_fraction")]
		public string CloudAreaFraction { get; set; }
		[JsonPropertyName("precipitation_amount")]
		public string PrecipitationAmount { get; set; }
		[JsonPropertyName("relative_humidity")]
		public string RelativeHumidity { get; set; }
		[JsonPropertyName("wind_from_direction")]
		public string WindFromDirection { get; set; }
		[JsonPropertyName("wind_speed")]
		public string WindSpeed { get; set; }
	}

	public class Timeseries
	{
		public DateTime Time { get; set; }
		public Data Data { get; set; }
	}

	public class Data
	{
		public Instant Instant { get; set; }
		[JsonPropertyName("next_12_hours")]
		public Next12Hours Next12Hours { get; set; }
		[JsonPropertyName("next_1_hours")]
		public Next1Hour Next1Hour { get; set; }
		[JsonPropertyName("next_6_hours")]
		public Next6Hours Next6Hours { get; set; }
	}

	public class Instant
	{
		public Details Details { get; set; }
	}

	public class Next12Hours
	{
		public Summary Summary { get; set; }
		public _Details Details { get; set; }
	}

	public class Next1Hour
	{
		public Summary Summary { get; set; }
		public _Details Details { get; set; }
	}

	public class Next6Hours
	{
		public Summary Summary { get; set; }
		public _Details Details { get; set; }
	}

	public class Summary
	{
		[JsonPropertyName("symbol_code")]
		public string SymbolCode { get; set; }
	}

	public class Details
	{
		[JsonPropertyName("air_pressure_at_sea_level")]
		public float AirPressureAtSeaLevel { get; set; }
		[JsonPropertyName("air_temperature")]
		public float AirTemperature { get; set; }
		[JsonPropertyName("cloud_area_fraction")]
		public float CloudAreaFraction { get; set; }
		[JsonPropertyName("relative_humidity")]
		public float RelativeHumidity { get; set; }
		[JsonPropertyName("wind_from_direction")]
		public float WindFromDirection { get; set; }
		[JsonPropertyName("wind_speed")]
		public float WindSpeed { get; set; }

	}
	public class _Details
	{
		[JsonPropertyName("precipitation_amount")]
		public float PrecipitationAmount { get; set; }
	}
}












