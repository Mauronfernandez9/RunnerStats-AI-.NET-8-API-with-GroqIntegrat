using RunnerStats.Data.Repositories;
using RunnerStats.Models.Dtos;
using RunnerStats.Models.Entities;
using RunnerStats.Services.Interfaces;

namespace RunnerStats.Services.Implementations
{
    public class NutritionService : INutritionService
    {
        private readonly INutritionRepository _nutritionRepository;

        public NutritionService(INutritionRepository nutrition)
        {
            _nutritionRepository = nutrition;
        }

        public async Task<DtoNutrition> GetDataNutrition(int runnerId)
        {
            var nutritionData = await _nutritionRepository.GetDataNutrition(runnerId);

            if (nutritionData != null)
            {
                DtoNutrition dtoNutrition = new()
                {
                    AdditionalNotes = nutritionData.AdditionalNotes,
                    GoalMaintainWeight = nutritionData.GoalMaintainWeight,
                    GoalMuscleGain = nutritionData.GoalMuscleGain,
                    GoalWeightLoss = nutritionData.GoalWeightLoss,
                    IsCeliac = nutritionData.IsCeliac,
                    IsDiabetic = nutritionData.IsDiabetic,
                    IsVegan = nutritionData.IsVegan,
                    LactoseIntolerant = nutritionData.LactoseIntolerant

                };
                return dtoNutrition;
                
            }
            return null!;

        }

        public async Task<bool> NewDataNutrition(DtoNutrition nutrition, int runnerId)
        {
            Nutrition nutritionNew = new()
            {
                RunnerId = runnerId,
                LactoseIntolerant = nutrition.LactoseIntolerant,
                IsVegan = nutrition.IsVegan,
                IsDiabetic = nutrition.IsDiabetic,
                IsCeliac = nutrition.IsCeliac,
                GoalWeightLoss = nutrition.GoalWeightLoss,
                GoalMuscleGain = nutrition.GoalMuscleGain,
                GoalMaintainWeight = nutrition.GoalMaintainWeight,
                AdditionalNotes = nutrition.AdditionalNotes
            };
            int response = await _nutritionRepository.NewDataNutrition(nutritionNew);
            if (response <= 0) 
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateDataNutrition(DtoNutrition dtoNutrition, int runnerId)
        {
            Nutrition dataNutritionToUpdate = await _nutritionRepository.GetDataNutrition(runnerId);
            if (dataNutritionToUpdate == null)
            {
                return false;
            }

            dataNutritionToUpdate.GoalMuscleGain = dtoNutrition.GoalMuscleGain;
            dataNutritionToUpdate.AdditionalNotes = dtoNutrition.AdditionalNotes != null ? dtoNutrition.AdditionalNotes : "" ;
            dataNutritionToUpdate.IsDiabetic = dtoNutrition.IsDiabetic;
            dataNutritionToUpdate.LactoseIntolerant = dtoNutrition.LactoseIntolerant;
            dataNutritionToUpdate.IsCeliac = dtoNutrition.IsCeliac;
            dataNutritionToUpdate.IsVegan = dtoNutrition.IsVegan;
            dataNutritionToUpdate.GoalMaintainWeight = dtoNutrition.GoalMaintainWeight;
            dataNutritionToUpdate.GoalWeightLoss = dtoNutrition.GoalWeightLoss;


            var responde = await _nutritionRepository.UpdateDataNutrition(dataNutritionToUpdate);

            if(responde <= 0)
            {
                return false;
            }

            return true;


        }
    }
}
