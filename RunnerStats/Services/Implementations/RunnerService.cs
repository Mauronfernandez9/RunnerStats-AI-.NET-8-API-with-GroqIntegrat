using System.Data.SqlTypes;
using RunnerStats.Data.Repositories;
using RunnerStats.Models.Dtos;
using RunnerStats.Models.Entities;
using RunnerStats.Services.Interfaces;

namespace RunnerStats.Services.Implementations
{
    public class RunnerService : IRunnerService
    {
        private IRunnerRepository _runnerRepository;

        public RunnerService(IRunnerRepository runnerRepository)
        {
            _runnerRepository = runnerRepository;
        }

        public async Task<int> createRunner(DtoRunner dtoRunner)
        {

            Runner runner = new()
            {
                Name = dtoRunner.Name,
                DateOfBirth = dtoRunner.DateOfBirth,
                Height = dtoRunner.Height,
                Weight = dtoRunner.Weight,
                Experience = dtoRunner.Experience,
            };


            var response =  await _runnerRepository.AddRunner(runner);


            return runner.IdRunner;
        }

        public async Task<Runner?> GetRunner(int id) 
        { 
            var runner = await _runnerRepository.GetRunner(id);
            return runner;
        
        }

        public async Task<bool> UpdateRunner(DtoRunner dtoRunner,int idRunner)
        {
            Runner runnerToUpdate = new()
            {
                DateOfBirth = dtoRunner.DateOfBirth,
                Experience = dtoRunner.Experience,
                Height = dtoRunner.Height,
                Name = dtoRunner.Name,
                TotalRaces = dtoRunner.TotalRaces,
                Weight = dtoRunner.Weight,
                IdRunner = idRunner
            };
            var response = await _runnerRepository.UpdateRunner(runnerToUpdate);
            if (response <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
