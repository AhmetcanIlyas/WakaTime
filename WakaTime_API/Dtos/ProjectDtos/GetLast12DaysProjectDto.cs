namespace WakaTime_API.Dtos.ProjectDtos
{
    public class GetLast12DaysProjectDto
    {
        public string ProjectName { get; set; }
        public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
    }
}
