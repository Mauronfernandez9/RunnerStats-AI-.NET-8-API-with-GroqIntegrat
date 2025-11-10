using System.ComponentModel.DataAnnotations;

namespace RunnerStats.Models.Dtos
{
    public class DtoRunner
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public int? Experience { get; set; }
        public int? TotalRaces { get; set; }
    }
}
