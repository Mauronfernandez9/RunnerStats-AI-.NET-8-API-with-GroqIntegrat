using System.ComponentModel.DataAnnotations;

namespace RunnerStats.Models.Entities
{
    public class Nutrition
    {
        [Key]
        public int NutritionId { get; set; }
        public bool IsVegan { get; set; }
        public bool IsCeliac { get; set; }
        public bool IsDiabetic { get; set; }
        public bool LactoseIntolerant { get; set; }
        public bool GoalWeightLoss { get; set; }
        public bool GoalMuscleGain { get; set; }
        public bool GoalMaintainWeight { get; set; }

        [MaxLength(200)]
        public string AdditionalNotes { get; set; }
        public int RunnerId { get; set; }
        public Runner Runner { get; set; }
    }
}
