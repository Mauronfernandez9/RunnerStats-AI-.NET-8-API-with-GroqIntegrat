using System.ComponentModel.DataAnnotations;

namespace RunnerStats.Models.Entities
{
    public class Runner
    {
        [Key]
        public int IdRunner { get; set; }
        [MaxLength(30)]
       
        public string Name { get; set; }
       
        public DateOnly DateOfBirth { get; set; }
        public double? Weight { get; set; }
        public double? Height {  get; set; }
        public int? Experience { get; set; }
        public int? TotalRaces { get; set; }

        public User User { get; set; }

        public Nutrition Nutrition { get; set; }
        public List<Race> Races { get; set; }

    }
}
