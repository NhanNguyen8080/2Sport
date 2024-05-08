using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using System.Linq.Expressions;

namespace _2Sport_BE.Infrastructure.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>> where, string? includes = "");
        Task AddAsync(User user);
        Task AddRangeAsync(IEnumerable<User> users);
        Task UpdateAsync(User user);
        Task RemoveAsync(int id);
        Task<bool> CheckExistAsync(int id);

    }
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task AddAsync(User user)
        {
           await unitOfWork.UserRepository.InsertAsync(user);
        }

        public Task AddRangeAsync(IEnumerable<User> users)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckExistAsync(int id)
        {
            IEnumerable<User> users = await unitOfWork.UserRepository.GetAsync(_ => _.Id == id);
            if (users.Any())
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            IEnumerable<User> users = await unitOfWork.UserRepository.GetAllAsync();
            return users;
        }

        public async Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>> where, string? includes = "")
        {
            IEnumerable<User> users = await unitOfWork.UserRepository.GetAsync(where, null, includes);
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await unitOfWork.UserRepository.GetByIDAsync(id);
            return user;
        }

        public async Task RemoveAsync(int id)
        {
           await unitOfWork.UserRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            await unitOfWork.UserRepository.UpdateAsync(user);
        }
    }
}
