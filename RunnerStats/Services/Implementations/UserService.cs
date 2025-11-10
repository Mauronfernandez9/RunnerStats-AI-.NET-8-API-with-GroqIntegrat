using Azure.Identity;
using RunnerStats.Data.Repositories;
using RunnerStats.Helpers;
using RunnerStats.Models.Dtos;
using RunnerStats.Models.Entities;
using RunnerStats.Services.Interfaces;

namespace RunnerStats.Services.Implementations
{
    public class UserService : IUserService

    {
        private readonly Utilities _utilities;
        private readonly IUserRepository _userRepository;

        public UserService(Utilities utilities, IUserRepository userRepository)
        {
            _utilities = utilities;
            _userRepository = userRepository;
        }

        public async Task<User> AuthenticateUser(DtoLogin login)
        {

            User user = await _userRepository.GetByEmail(login.Email!);
            if (user == null) 
            {
                return null!;
            }

            bool isValid = user.PasswordHash == _utilities.EncryptSha256(login.Password!);
            return isValid ? user : null!;
        }

        public async Task<bool> CreateNewUser(DtoUser newUser)
        {

            bool response = await _userRepository.ExistByEmail(newUser.Email);
            if (response)
            {
                return false;

            }
            User userToCreate = new()
            {
                Email = newUser.Email!,
                PasswordHash = _utilities.EncryptSha256(newUser.PasswordHash!),
                RunnerId = newUser.RunnerId

            };

            int result = await _userRepository.AddUser(userToCreate);
            if (result > 0)
            {
                return true;
            }
            return false;
            
        }


    }
}
