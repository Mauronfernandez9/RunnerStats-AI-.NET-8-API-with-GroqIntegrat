using RunnerStats.Models.Dtos;
using RunnerStats.Models.Entities;

namespace RunnerStats.Data.Repositories
{
    public interface INutritionRepository
    {
        Task<Nutrition> GetDataNutrition(int runnerId);

        Task<int> NewDataNutrition(Nutrition newDataNutrition);

        Task<int> UpdateDataNutrition(Nutrition newDataNutrition);
    }
}
