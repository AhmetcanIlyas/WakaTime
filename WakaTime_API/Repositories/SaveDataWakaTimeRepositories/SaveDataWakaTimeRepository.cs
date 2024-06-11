using WakaTime_API.Models.DapperContext;
using WakaTime_API.Repositories.CategoryRepositories;
using WakaTime_API.Repositories.DailyUserActivityRepositories;
using WakaTime_API.Repositories.DependenceRepositories;
using WakaTime_API.Repositories.EditorRepositories;
using WakaTime_API.Repositories.LanguageRepositories;
using WakaTime_API.Repositories.MachineRepositories;
using WakaTime_API.Repositories.OperatingSystemRepositories;
using WakaTime_API.Repositories.ProjectRepositories;
using WakaTime_API.Repositories.UserActivityRepositories;
using WakaTime_API.Repositories.UsersRepositories;
using WakaTime_API.Repositories.WakaTimeRepositories;

namespace WakaTime_API.Repositories.SaveDataWakaTimeRepositories
{
	public class SaveDataWakaTimeRepository : ISaveDataWakaTimeRepository
	{
		private readonly Context _context;
		private readonly IWakaTimeRepository _wakaTimeRepository;
		private readonly IUsersRepository _usersRepository;
		private readonly IUserActivityRepository _userActivityRepository;
		private readonly IProjectRepository _projectRepository;
		private readonly IOperatingSystemRepository _operatingSystemRepository;
		private readonly IMachineRepository _machineRepository;
		private readonly ILanguageRepository _languageRepository;
		private readonly IEditorRepository _editorRepository;
		private readonly IDependenceRepository _dependenceRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IDailyUserActivityRespository _dailyUserActivityRespository;
        public SaveDataWakaTimeRepository(Context context, IWakaTimeRepository wakaTimeRepository, IUsersRepository usersRepository,
            IUserActivityRepository userActivityRepository, IProjectRepository projectRepository, IOperatingSystemRepository operatingSystemRepository,
            IMachineRepository machineRepository, ILanguageRepository languageRepository, IEditorRepository editorRepository,
            IDependenceRepository dependenceRepository, ICategoryRepository categoryRepository, IDailyUserActivityRespository dailyUserActivityRespository)
        {
            _context = context;
            _wakaTimeRepository = wakaTimeRepository;
            _usersRepository = usersRepository;
            _userActivityRepository = userActivityRepository;
            _projectRepository = projectRepository;
            _operatingSystemRepository = operatingSystemRepository;
            _machineRepository = machineRepository;
            _languageRepository = languageRepository;
            _editorRepository = editorRepository;
            _dependenceRepository = dependenceRepository;
            _categoryRepository = categoryRepository;
            _dailyUserActivityRespository = dailyUserActivityRespository;
        }
        public async Task SaveDataForAllUser()
		{
			var users = await _usersRepository.GetAllUsersAsync();
			foreach (var user in users)
			{
				var startDate = user.StartDate;
				var apiKey = user.ApiKey;
				var userId = user.UserId;
				var responseWakaTime = await _wakaTimeRepository.GetWakaTimeResponseAsync(startDate, apiKey);
				var totalSeconds = responseWakaTime.cumulative_total.seconds;
				var totalTimeText = responseWakaTime.cumulative_total.text;
				var dailyAverageSeconds = responseWakaTime.daily_average.seconds;
				var dailyTimeText = responseWakaTime.daily_average.text;
				await _userActivityRepository.UpdateUserActivityAsync(userId, totalSeconds, totalTimeText, dailyAverageSeconds, dailyTimeText);

				foreach (var itemData in responseWakaTime.data)
				{
					var dateTimeValue = itemData.range.end;
					var dateOnly = dateTimeValue.Date;
					var grandTotalSeconds = itemData.grand_total.total_seconds;
					var timeText = itemData.grand_total.text;
					await _dailyUserActivityRespository.SaveDailyUserActivityAsync(userId,grandTotalSeconds,timeText,dateOnly);

					foreach (var itemLanguages in itemData.languages)
					{
						var languageName = itemLanguages.name;
						var languageTime = itemLanguages.total_seconds;
						var languageTimeText = itemLanguages.text;
						await _languageRepository.SaveLanguageAsync(userId, languageName, languageTime,languageTimeText, dateOnly);
					}
					foreach (var itemEditors in itemData.editors)
					{
						var editorName = itemEditors.name;
						var editorTime = itemEditors.total_seconds;
						var editorTimeText = itemEditors.text;
						await _editorRepository.SaveEditorAsync(userId, editorName, editorTime, editorTimeText, dateOnly);
					}
					foreach (var itemOperatingSystems in itemData.operating_systems)
					{
						var operatingSystemName = itemOperatingSystems.name;
						var operatingTime = itemOperatingSystems.total_seconds;
						var operatingTimeText = itemOperatingSystems.text;
						await _operatingSystemRepository.SaveOperatingSystemAsync(userId, operatingSystemName, operatingTime, operatingTimeText, dateOnly);
					}
					foreach (var itemCategories in itemData.categories)
					{
						var categoryName = itemCategories.name;
						var categoryTime = itemCategories.total_seconds;
						var categoryTimeText = itemCategories.text;
						await _categoryRepository.SaveCategoryAsync(userId, categoryName, categoryTime, categoryTimeText, dateOnly);
					}
					foreach (var itemDependencies in itemData.dependencies)
					{
						var dependenciesName = itemDependencies.name;
						var dependenciesTime = itemDependencies.total_seconds;
						var dependenciesTimeText = itemDependencies.text;
						await _dependenceRepository.SaveDependenceAsync(userId, dependenciesName, dependenciesTime, dependenciesTimeText, dateOnly);
					}
					foreach (var itemMachines in itemData.machines)
					{
						var machineName = itemMachines.name;
						var machineTime = itemMachines.total_seconds;
						var machineId = itemMachines.machine_name_id;
						var machineTimeText = itemMachines.text;
						await _machineRepository.SaveMachineAsync(userId, machineName, machineId, machineTime, machineTimeText, dateOnly);
					}
					foreach (var itemProjects in itemData.projects)
					{
						var projectName = itemProjects.name;
						var projectTime = itemProjects.total_seconds;
						var projectTimeText = itemProjects.text;
						await _projectRepository.SaveProjectAsync(userId, projectName, projectTime, projectTimeText, dateOnly);
					}
                }
			}
		}
	}
}
