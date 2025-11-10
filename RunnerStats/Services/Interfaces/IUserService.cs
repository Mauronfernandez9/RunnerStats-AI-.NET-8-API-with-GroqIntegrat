using RunnerStats.Models.Dtos;
using RunnerStats.Models.Entities;

namespace RunnerStats.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateNewUser(DtoUser newUser);
        Task<User> AuthenticateUser(DtoLogin login);
    }
}
