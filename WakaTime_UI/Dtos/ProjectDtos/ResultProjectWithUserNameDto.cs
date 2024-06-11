﻿namespace WakaTime_UI.Dtos.ProjectDtos
{
    public class ResultProjectWithUserNameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }
}
