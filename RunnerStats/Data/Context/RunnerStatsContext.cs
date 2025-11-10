using Microsoft.EntityFrameworkCore;
using RunnerStats.Models.Entities;

namespace RunnerStats.Data.Context
{
    public class RunnerStatsContext : DbContext
    {
        
        public RunnerStatsContext(DbContextOptions<RunnerStatsContext> options) : base (options) 
        { 
            
                    
        }
     

        public DbSet<Nutrition> Nutritions { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Runner> Runners { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
