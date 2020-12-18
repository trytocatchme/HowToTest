using HowToTest.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using HowToTest.Infrastructure.Services;
using System.Threading.Tasks;
using HowToTest.Infrastructure.Tests.Extensions;

namespace HowToTest.Infrastructure.Tests
{
    public class UserServiceTest
    {
        [Fact]
        public async Task GetAdultUsersAsync_AgeLessThan18_EmptyCollection()
        {
            // Arrange
            var notAdultUsers = new List<User> 
            { 
                new User { Id = 1, Age = 1 },
                new User { Id = 2, Age = 12 },
                new User { Id = 3, Age = 17 },
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.SetupDbSet(notAdultUsers);
            
            var context = new Mock<ApplicationContext>();
            context.Setup(x => x.Users).Returns(dbSetMock.Object);

            // Act
            var userService = new UserService(context.Object);
            var result = await userService.GetAdultUsersAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}