namespace WakaTime_API.Dtos.CategoryDtos
{
	public class ResultCategoryDto
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
    }
}
