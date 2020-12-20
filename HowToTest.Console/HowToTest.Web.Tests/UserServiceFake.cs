using HowToTest.Infrastructure.Models;
using HowToTest.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToTest.Web.Tests
{
    public class UserServiceFake : IUserService
    {
        private readonly List<User> _users;

        public UserServiceFake()
        {
            _users = new List<User>()
            {
                new User() { Id = 1, Age = 10, Name="Artur", Surname = "Surname1"},
                new User() { Id = 2, Age = 18, Name="Michal", Surname = "Surname2"},
                new User() { Id = 3, Age = 20, Name="pawel", Surname = "Surname3"}
            };
        }

        public Task<int> AddAsync(User user)
        {
          //  _users.ad
            throw new NotImplementedException();
        }

        public Task<int> AddOnlyIfAdultAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAdultUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task RemoveAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
