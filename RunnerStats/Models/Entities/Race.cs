using System.ComponentModel.DataAnnotations;

namespace RunnerStats.Models.Entities
{
    public class Race
    {
        [Key]
        public int IdRace { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Location { get; set; }
        public double DistanceKm { get; set; }
        public double DurationMinutes { get; set; }
        public bool Completed { get; set; }
        public int RunnerId { get; set; }
        public Runner Runner { get; set; }

    }
}
