﻿namespace WakaTime_UI.Dtos.MachineDtos
{
	public class GetAllMachineByUserIdDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string MachineName { get; set; }
		public string MachineId { get; set; }
		public float TotalDailySeconds { get; set; }
        public string TimeText { get; set; }
        public DateTime Date { get; set; }
	}
}
