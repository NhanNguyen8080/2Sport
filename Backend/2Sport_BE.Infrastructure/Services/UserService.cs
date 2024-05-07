using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using System.Linq.Expressions;

namespace _2Sport_BE.Infrastructure.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        IEnumerable<User> GetAll();
        IEnumerable<User> Get(Expression<Func<User, bool>> where, string? includes = "");
        void Add(User user);
        void AddRange(IEnumerable<User> users);
        void Update(User user);
        void Remove(int Id);
        bool CheckExist(int Id);

    }
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(User user)
        {
            unitOfWork.UserRepository.Insert(user);
        }

        public void AddRange(IEnumerable<User> users)
        {
            throw new NotImplementedException();
        }

        public bool CheckExist(int Id)
        {
            IEnumerable<User> users = unitOfWork.UserRepository.Get(_ => _.Id == Id);
            if (users.Any())
            {
                return true;
            }
            return false;
        }

        public IEnumerable<User> Get(Expression<Func<User, bool>> where, string? includes = "")
        {
            IEnumerable<User> users = unitOfWork.UserRepository.Get(where, null, includes);
            return users;
        }

        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users = unitOfWork.UserRepository.GetAll();
            return users;

        }

        public User GetUserById(int id)
        {
            var user = (User)unitOfWork.UserRepository.GetByID(id);
            return user;
        }

        public void Remove(int Id)
        {
            unitOfWork.UserRepository.Delete(Id);
        }

        public void Update(User user)
        {
            unitOfWork.UserRepository.Update(user);
        }
    }
}
