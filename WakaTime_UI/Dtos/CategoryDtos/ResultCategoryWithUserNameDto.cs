namespace WakaTime_UI.Dtos.CategoryDtos
{
    public class ResultCategoryWithUserNameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        
    }
}
