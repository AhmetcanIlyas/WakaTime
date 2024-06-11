namespace WakaTime_API.Dtos.WakaTimeDto
{
	public class WakaTimeResponseDto
	{

		public class Rootobject
		{
			public Datum[] data { get; set; }
			public DateTime start { get; set; }
			public DateTime end { get; set; }
			public Cumulative_Total cumulative_total { get; set; }
			public Daily_Average daily_average { get; set; }
		}

		public class Cumulative_Total
		{
			public float seconds { get; set; }
			public string text { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
		}

		public class Daily_Average
		{
			public int holidays { get; set; }
			public int days_minus_holidays { get; set; }
			public int days_including_holidays { get; set; }
			public int seconds { get; set; }
			public int seconds_including_other_language { get; set; }
			public string text { get; set; }
			public string text_including_other_language { get; set; }
		}

		public class Datum
		{
			public Language[] languages { get; set; }
			public Grand_Total grand_total { get; set; }
			public Editor[] editors { get; set; }
			public Operating_Systems[] operating_systems { get; set; }
			public Category[] categories { get; set; }
			public Dependency[] dependencies { get; set; }
			public Machine[] machines { get; set; }
			public Project[] projects { get; set; }
			public Range range { get; set; }
		}

		public class Grand_Total
		{
			public int hours { get; set; }
			public int minutes { get; set; }
			public float total_seconds { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
			public string text { get; set; }
		}

		public class Range
		{
			public DateTime start { get; set; }
			public DateTime end { get; set; }
			public string date { get; set; }
			public string text { get; set; }
			public string timezone { get; set; }
		}

		public class Language
		{
			public string name { get; set; }
			public float total_seconds { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
			public string text { get; set; }
			public int hours { get; set; }
			public int minutes { get; set; }
			public int seconds { get; set; }
			public float percent { get; set; }
		}

		public class Editor
		{
			public string name { get; set; }
			public float total_seconds { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
			public string text { get; set; }
			public int hours { get; set; }
			public int minutes { get; set; }
			public int seconds { get; set; }
			public float percent { get; set; }
		}

		public class Operating_Systems
		{
			public string name { get; set; }
			public float total_seconds { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
			public string text { get; set; }
			public int hours { get; set; }
			public int minutes { get; set; }
			public int seconds { get; set; }
			public float percent { get; set; }
		}

		public class Category
		{
			public string name { get; set; }
			public float total_seconds { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
			public string text { get; set; }
			public int hours { get; set; }
			public int minutes { get; set; }
			public int seconds { get; set; }
			public float percent { get; set; }
		}

		public class Dependency
		{
			public string name { get; set; }
			public float total_seconds { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
			public string text { get; set; }
			public int hours { get; set; }
			public int minutes { get; set; }
			public int seconds { get; set; }
			public float percent { get; set; }
		}

		public class Machine
		{
			public string name { get; set; }
			public float total_seconds { get; set; }
			public string machine_name_id { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
			public string text { get; set; }
			public int hours { get; set; }
			public int minutes { get; set; }
			public int seconds { get; set; }
			public float percent { get; set; }
		}

		public class Project
		{
			public string name { get; set; }
			public float total_seconds { get; set; }
			public object color { get; set; }
			public string digital { get; set; }
			public string _decimal { get; set; }
			public string text { get; set; }
			public int hours { get; set; }
			public int minutes { get; set; }
			public int seconds { get; set; }
			public float percent { get; set; }
		}

	}
}
