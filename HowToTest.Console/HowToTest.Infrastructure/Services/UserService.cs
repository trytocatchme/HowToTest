using HowToTest.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowToTest.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private const int adultsAge = 18;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAdultUsersAsync()
        {
            return await _context.Users.Where(x => x.Age >= adultsAge).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}