using WakaTime_API.Dtos.WakaTimeDto;

namespace WakaTime_API.Repositories.WakaTimeRepositories
{
	public interface IWakaTimeRepository
	{
		Task<WakaTimeResponseDto.Rootobject> GetWakaTimeResponseAsync(DateTime startDate, string ApiKey);
	}
}
