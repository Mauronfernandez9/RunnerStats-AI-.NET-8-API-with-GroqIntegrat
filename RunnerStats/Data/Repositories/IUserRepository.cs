using RunnerStats.Models.Entities;

namespace RunnerStats.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetById(int id);
        Task<bool> ExistByEmail(string email);
        Task<int> AddUser(User user);
    }
}
