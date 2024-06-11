using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WakaTime_API.Dtos.WakaTimeDto;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.WakaTimeRepositories
{
	public class WakaTimeRepository : IWakaTimeRepository
	{
		private readonly Context _context;
		private readonly IHttpClientFactory _httpClientFactory;
		public WakaTimeRepository(Context context, IHttpClientFactory httpClientFactory)
		{
			_context = context;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<WakaTimeResponseDto.Rootobject> GetWakaTimeResponseAsync(DateTime startDate, string ApiKey)
		{
			DateTime endDate = DateTime.Now;
			using (var client = _httpClientFactory.CreateClient())
			{
				var request = new HttpRequestMessage(HttpMethod.Get,
				$"https://wakatime.com/api/v1/users/current/summaries?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}");
				request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(ApiKey)));
				var responseMessage = await client.SendAsync(request);
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<WakaTimeResponseDto.Rootobject>(jsonData);
					return values;
				}
				else
				{
					throw new HttpRequestException($"Request failed with status code: {responseMessage.StatusCode}");
				}
			}
		}
	}
}