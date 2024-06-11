namespace WakaTime_API.Dtos.DailyUserActivityDtos
{
    public class ResultDailyUserActivityDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float GeneralTotalDailyActivitySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
    }
}
