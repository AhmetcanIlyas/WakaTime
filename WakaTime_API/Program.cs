using WakaTime_API.Models.DapperContext;
using WakaTime_API.Repositories.CategoryRepositories;
using WakaTime_API.Repositories.DailyUserActivityRepositories;
using WakaTime_API.Repositories.DependenceRepositories;
using WakaTime_API.Repositories.EditorRepositories;
using WakaTime_API.Repositories.LanguageRepositories;
using WakaTime_API.Repositories.LoginRepositories;
using WakaTime_API.Repositories.MachineRepositories;
using WakaTime_API.Repositories.OperatingSystemRepositories;
using WakaTime_API.Repositories.ProjectRepositories;
using WakaTime_API.Repositories.SaveDataWakaTimeRepositories;
using WakaTime_API.Repositories.StatisticsRepositories;
using WakaTime_API.Repositories.UserActivityRepositories;
using WakaTime_API.Repositories.UsersRepositories;
using WakaTime_API.Repositories.WakaTimeRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddTransient<Context>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IWakaTimeRepository, WakaTimeRepository>();
builder.Services.AddTransient<IUserActivityRepository, UserActivityRepository>();
builder.Services.AddTransient<ISaveDataWakaTimeRepository, SaveDataWakaTimeRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IOperatingSystemRepository, OperatingSystemRepository>();
builder.Services.AddTransient<IMachineRepository, MachineRepository>();
builder.Services.AddTransient<ILanguageRepository, LanguageRepository>();
builder.Services.AddTransient<IEditorRepository, EditorRepository>();
builder.Services.AddTransient<IDependenceRepository, DependenceRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IDailyUserActivityRespository, DailyUserActivityRespository>();
builder.Services.AddTransient<IStatisticsRepository, StatisticsRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
