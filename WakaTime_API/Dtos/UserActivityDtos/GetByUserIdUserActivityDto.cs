namespace WakaTime_API.Dtos.UserActivityDtos
{
	public class GetByUserIdUserActivityDto
	{
        public int UserId { get; set; }
        public float TotalSeconds { get; set; }
        public string TotalTimeText { get; set; }
        public int DailyAverageSeconds { get; set; }
        public string DailyTimeText { get; set; }
    }
}
