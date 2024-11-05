using BookShopBusiness;
using BookShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDAO;

        public UserRepository(BookDbContext context)
        {
            _userDAO = new UserDAO(context);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userDAO.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userDAO.GetUserById(id);
        }

        public void AddUser(User user)
        {
            _userDAO.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            _userDAO.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _userDAO.DeleteUser(id);
        }
    }

}
