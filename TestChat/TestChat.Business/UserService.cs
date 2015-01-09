using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestChat.dal;
using TestChat.Model;

namespace TestChat.Business
{
    public interface IUserService
    {
        IList<User> GetAll();
        User GetByConnectionID(string connectionID);
        User GetByUserName(string userName);
        User GetByID(long ID);
        void Add(User obj);
        void Update(User obj);
        void Remove(User obj);
    }
    public class UserService : IUserService
    {
        IUserRepository repository;
        public UserService()
        {
            repository = new UserRepository();
        }
        public IList<User> GetAll()
        {
            return repository.GetAll();
        }

        public User GetByConnectionID(string connectionID)
        {
            return repository.GetSingle(r => r.ConnectionID.Equals(connectionID));
        }

        public User GetByUserName(string userName)
        {
            return repository.GetSingle(r => r.UserName.Equals(userName));
        }

        public User GetByID(long ID)
        {
            return repository.GetSingle(r => r.UserID.Equals(ID));
        }

        public void Add(User obj)
        {
            repository.Add(obj);
        }

        public void Update(User obj)
        {
            repository.Update(obj);
        }

        public void Remove(User obj)
        {
            repository.Remove(obj);
        }

        public void AddUser(string userName, string connectionID)
        {
            var user = GetByUserName(userName);

            if (user != null)
            {
                //Returning user. Just change connection id
                user.ConnectionID = connectionID;
                Update(user);
            }
            else
            {
                user = new User();
                user.UserName = userName;
                user.ConnectionID = connectionID;
                Add(user);
            }
        }

        public string GetUserNameByConnectionID(string connectionID)
        {
            var user = GetByConnectionID(connectionID);

            return user != null ? user.UserName : string.Empty;
        }

        public void RemoveUser(string connectionID)
        {
            var user = GetByConnectionID(connectionID);

            if (user != null)
                Remove(user);
        }
    }
}
