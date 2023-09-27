namespace Mytask.API.Model;

public interface IStageRepository
{
    Task<List<Stage>> GetStagesAsync(List<string> boardStages);
    Task<Stage> CreateStageAsync(Stage stage);
    Task<Stage> UpdateStageAsync(Stage stage);
    Task<bool> DeleteStageAsync(string id);
}