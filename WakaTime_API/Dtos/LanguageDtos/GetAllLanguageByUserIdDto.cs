﻿namespace WakaTime_API.Dtos.LanguageDtos
{
	public class GetAllLanguageByUserIdDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string LanguageName { get; set; }
		public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
	}
}
