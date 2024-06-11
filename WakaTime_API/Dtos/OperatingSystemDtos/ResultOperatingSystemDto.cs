﻿namespace WakaTime_API.Dtos.OperatingSystemDtos
{
	public class ResultOperatingSystemDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string OperatingSystemName { get; set; }
		public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
	}
}
