﻿namespace WakaTime_UI.Dtos.EditorDtos
{
    public class ResultEditorWithUserNameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EditorName { get; set; }
        public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }
}
