using HowToTest.Infrastructure.Models;
using HowToTest.Infrastructure.Services;
using HowToTest.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HowToTest.Web.Tests
{
    public class UserControllerTest
    {
        private readonly IUserService _userService;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _userService = new UserServiceFake();
            _userController = new UserController(_userService);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            const int expectedNumerOfUsers = 3;

            // Act
            var okResult = await _userController.GetAllAsync();
            var result = okResult.Result as OkObjectResult;
            var returnedUsers = result.Value as IList<User>;
            // Assert
            Assert.Equal(expectedNumerOfUsers, returnedUsers.Count);
        }
    }
}
