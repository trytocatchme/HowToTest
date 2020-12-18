﻿using HowToTest.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowToTest.Infrastructure.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAdultUsersAsync();
        Task<IEnumerable<User>> GetAllAsync();
    }
}
