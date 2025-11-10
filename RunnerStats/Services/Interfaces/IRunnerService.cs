using RunnerStats.Models.Dtos;
using RunnerStats.Models.Entities;

namespace RunnerStats.Services.Interfaces

{
    public interface IRunnerService
    {
        Task<int> createRunner(DtoRunner dtoRunner);

        public  Task<Runner?> GetRunner(int id);

        public Task<bool> UpdateRunner(DtoRunner dtoRunner,int idRunner);




    }
}
