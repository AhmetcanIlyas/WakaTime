namespace WakaTime_API.Dtos.DependenceDtos
{
    public class ResultDependenceWithUserNameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DependenceName { get; set; }
        public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }
}
