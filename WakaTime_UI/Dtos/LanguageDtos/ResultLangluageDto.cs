namespace WakaTime_UI.Dtos.LanguageDtos
{
	public class ResultLangluageDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string LanguageName { get; set; }
		public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
	}
}
