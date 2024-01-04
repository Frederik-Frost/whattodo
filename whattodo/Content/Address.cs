using System;
using whattodo.Utilitites;
using System.Text.Json;

namespace whattodo.Content
{
	public class Address
	{
		string BaseUrl = "https://api.dataforsyningen.dk/";
		// public Address()
		// {
		// }

		public async Task<PostnummerResult[]> SearchCity(string query)
		{
			string endpoint = $"postnumre/autocomplete?q={query}";
			HttpHelper client = new HttpHelper(BaseUrl);

			var res = await client.GetAsync(new Uri(BaseUrl + endpoint));
			var content = await res.Content.ReadAsStringAsync();
			var json = JsonSerializer.Deserialize<PostnummerResult[]>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return json;

		}


		public async Task<CityData> GetCityFromPostalCode(string postalCode)
		{
			string endpoint = $"postnumre/{postalCode}";
			HttpHelper client = new HttpHelper(BaseUrl);

			var res = await client.GetAsync(new Uri(BaseUrl + endpoint));
			var content = await res.Content.ReadAsStringAsync();
			var json = JsonSerializer.Deserialize<CityData>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return json;
		}


		public static List<CustomSearchResult> ConvertToIC(PostnummerResult[] postnummerResult)
		{
			List<CustomSearchResult> customList = new List<CustomSearchResult>();

			foreach (var result in postnummerResult)
			{
				CustomSearchResult customObj = new CustomSearchResult
				{
					Id = result.Postnummer.Nr,
					ReferenceName = result.Postnummer.Navn,
					ReferenceDescription = result.Tekst,
					Time = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
				};

				customList.Add(customObj);
			}

			return customList;
		}
	}
	public class PostnummerResult
	{
		public string Tekst { get; set; }
		public Postnummer Postnummer { get; set; }
	}
	public class Postnummer
	{
		public string Nr { get; set; }
		public string Navn { get; set; }
		public bool Stormodtager { get; set; }
		public float Visueltcenter_x { get; set; }
		public float Visueltcenter_y { get; set; }
		public string Href { get; set; }

	}
}

