using Microsoft.EntityFrameworkCore;
using RunnerStats.Data.Context;
using RunnerStats.Data.Repositories;
using RunnerStats.Models.Entities;

namespace RunnerStats.Data.Implementations
{
    public class RunnerRepository : IRunnerRepository
    {

        private readonly RunnerStatsContext _context;

        public RunnerRepository(RunnerStatsContext context)
        {
            _context = context;
        }

        public async Task<int> AddRunner(Runner newRunner)
        {
            await _context.Runners.AddAsync(newRunner);
            return await _context.SaveChangesAsync();

        }

        public async Task<Runner> GetRunner(int id)
        {
            return (await _context.Runners.FirstOrDefaultAsync(r => r.IdRunner == id))!;

        }

        public async Task<int> UpdateRunner(Runner runner)
        {
            var runnerToUpdate = await _context.Runners.FirstOrDefaultAsync(r => r.IdRunner == runner.IdRunner);
            if (runnerToUpdate == null) {
                return 0;
            
            }
            runnerToUpdate.Weight = runner.Weight;
            runnerToUpdate.Height = runner.Height;
            runnerToUpdate.Experience = runner.Experience;
            runnerToUpdate.DateOfBirth = runner.DateOfBirth;
            runnerToUpdate.Name = runner.Name;
            runnerToUpdate.TotalRaces = runner.TotalRaces;
            return await _context.SaveChangesAsync();
        }
    }
}
