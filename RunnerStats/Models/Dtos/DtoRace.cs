using System.ComponentModel.DataAnnotations;

namespace RunnerStats.Models.Dtos
{
    public class DtoRace
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Location { get; set; }
        public double DistanceKm { get; set; }
        public double DurationMinutes { get; set; }
        public bool Completed { get; set; }
    }
}
