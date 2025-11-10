using RunnerStats.Models.Entities;

namespace RunnerStats.Data.Repositories
{
    public interface IRunnerRepository
    {
        Task<int> AddRunner(Runner newRunner);
        public  Task<Runner> GetRunner(int id);

        public Task<int> UpdateRunner(Runner runner);
    }
}
