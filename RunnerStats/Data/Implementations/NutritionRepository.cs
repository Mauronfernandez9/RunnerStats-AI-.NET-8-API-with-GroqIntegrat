using Microsoft.EntityFrameworkCore;
using RunnerStats.Data.Context;
using RunnerStats.Data.Repositories;
using RunnerStats.Models.Entities;

namespace RunnerStats.Data.Implementations
{
    public class NutritionRepository : INutritionRepository
    {
        private readonly RunnerStatsContext _context;

        public NutritionRepository(RunnerStatsContext context)
        {
            _context = context;
        }

        public async Task<Nutrition> GetDataNutrition(int runnerId)
        {
          return (await _context.Nutritions.FirstOrDefaultAsync(n => n.RunnerId == runnerId))!;

        }

        public async Task<int> NewDataNutrition(Nutrition newDataNutrition)
        {
            await _context.Nutritions.AddAsync(newDataNutrition);
            return _context.SaveChanges();
        }

        public async Task<int> UpdateDataNutrition(Nutrition newDataNutrition)
        {
            var dataNutritionToUpdate = await _context.Nutritions.FirstOrDefaultAsync(n => n.NutritionId ==  newDataNutrition.NutritionId);
            if (dataNutritionToUpdate != null)
            {
                dataNutritionToUpdate.LactoseIntolerant = newDataNutrition.LactoseIntolerant;
                dataNutritionToUpdate.IsDiabetic = newDataNutrition.IsDiabetic;
                dataNutritionToUpdate.GoalMuscleGain = newDataNutrition.GoalMuscleGain;
                dataNutritionToUpdate.GoalMaintainWeight = newDataNutrition.GoalMaintainWeight;
                dataNutritionToUpdate.GoalWeightLoss = newDataNutrition.GoalWeightLoss;
                dataNutritionToUpdate.AdditionalNotes = newDataNutrition.AdditionalNotes;
                dataNutritionToUpdate.IsCeliac = newDataNutrition.IsCeliac;
                dataNutritionToUpdate.IsVegan = newDataNutrition.IsVegan;
                return await _context.SaveChangesAsync();
            }
            return 0;

        }
    }
}
