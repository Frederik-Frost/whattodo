using System;
using System.Net.Http.Headers;
namespace whattodo.Utilitites
{
	public class HttpHelper
	{
		public Uri BaseAddress { get; set; }
		private HttpClient Client { get; set; }

		public HttpHelper(string baseAddress)
		{
			this.BaseAddress = new Uri(baseAddress);
			Client = new HttpClient{ BaseAddress = BaseAddress };
			Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			Client.DefaultRequestHeaders.UserAgent.TryParseAdd("whattodo #App");
		}

		public async Task<HttpResponseMessage> GetAsync(Uri uri)
		{
			return await Client.GetAsync(uri);
		}
	}
}

