using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using RunnerStats.Data.Context;
using RunnerStats.Data.Repositories;
using RunnerStats.Models.Entities;

namespace RunnerStats.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly RunnerStatsContext _context;

        public UserRepository(RunnerStatsContext context)
        {
            _context = context;
        }

        public async Task<int> AddUser(User user)
        {
            await _context.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistByEmail(string email)
        {
            bool respuesta = (await _context.Users.FirstOrDefaultAsync(u => u.Email == email)) != null;
            return respuesta;

        }

        public async Task<User> GetByEmail(string email)
        {
            User user = (((await _context.Users.FirstOrDefaultAsync(u => u.Email == email))!));
            return user;
        }

        public async Task<User> GetById(int id)
        {
            User user = (((await _context.Users.FirstOrDefaultAsync(u => u.IdUser == id))!));
            return user;
        }
    }
}
