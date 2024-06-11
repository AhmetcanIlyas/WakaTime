namespace WakaTime_API.Repositories.LoginRepositories
{
    public interface ILoginRepository
    {
        Task<bool> LoginControlAsync(string email, string password);
    }
}
