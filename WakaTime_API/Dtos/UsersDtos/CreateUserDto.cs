namespace WakaTime_API.Dtos.UsersDtos
{
	public class CreateUserDto
	{
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
		public DateTime StartDate { get; private set; }
		public string ApiKey { get; set; }
		public string PhoneNumber { get; set; }
		public string Gender { get; set; }
		public string Photo { get; set; }
    }
}
