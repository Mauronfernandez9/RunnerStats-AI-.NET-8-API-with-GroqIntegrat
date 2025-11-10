using RunnerStats.Models.Dtos;
namespace RunnerStats.Services.Interfaces
{
    public interface INutritionService
    {
        Task<DtoNutrition> GetDataNutrition(int runnerId);

        Task<bool> NewDataNutrition(DtoNutrition nutrition, int runnerId);

        Task<bool> UpdateDataNutrition(DtoNutrition dtoNutrition, int runnerId);
    }
}
