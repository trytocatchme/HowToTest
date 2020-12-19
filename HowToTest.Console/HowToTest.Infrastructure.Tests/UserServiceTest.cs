using HowToTest.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using HowToTest.Infrastructure.Services;
using System.Threading.Tasks;
using HowToTest.Infrastructure.Tests.Extensions;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

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

        [Fact]
        public async Task AddAsync_EmptyCollection_AddedItem()
        {
            // Arrange
            var emptyCollection = new List<User>();
            var testObject = new User { Id = 1, Age = 1 };
            var dbSetMock = new Mock<DbSet<User>>();

            var context = new Mock<ApplicationContext>();
            context
                .Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User model, CancellationToken token) => { emptyCollection.Add(model); })
                .Returns((User model, CancellationToken token) => new ValueTask<EntityEntry<User>>());

            // Act
            var userService = new UserService(context.Object);
            await userService.AddAsync(testObject);

            // Assert
            Assert.NotEmpty(emptyCollection);
        }

        [Fact]
        public async Task AddOnlyIfAdultAsync_EmptyCollection_NotAddedItem()
        {
            // Arrange
            var emptyCollection = new List<User>();
            var testObject = new User { Id = 1, Age = 1 };
            var dbSetMock = new Mock<DbSet<User>>();

            var context = new Mock<ApplicationContext>();
            context
                .Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User model, CancellationToken token) => { emptyCollection.Add(model); })
                .Returns((User model, CancellationToken token) => new ValueTask<EntityEntry<User>>());

            var userService = new UserService(context.Object);

            // Act
            Task act() => userService.AddOnlyIfAdultAsync(testObject);

            // Assert
            await Assert.ThrowsAsync<Exception>(act);
        }


        [Fact]
        public async Task AddOnlyIfAdultAsync_EmptyCollection_AddedItem()
        {
            // Arrange
            var emptyCollection = new List<User>();
            var testObject = new User { Id = 1, Age = 20 };
            var dbSetMock = new Mock<DbSet<User>>();

            var context = new Mock<ApplicationContext>();
            context
                .Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback((User model, CancellationToken token) => { emptyCollection.Add(model); })
                .Returns((User model, CancellationToken token) => new ValueTask<EntityEntry<User>>());

            var userService = new UserService(context.Object);

            // Act
            await userService.AddOnlyIfAdultAsync(testObject);

            // Assert
            Assert.NotEmpty(emptyCollection);
        }

        [Fact]
        public async Task Remove_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testObject = new User { Id = 1, Age = 20 };
            var emptyCollection = new List<User>() { testObject };
            
            var dbSetMock = new Mock<DbSet<User>>();

            var context = new Mock<ApplicationContext>();
            context
                .Setup(x => x.Remove(It.IsAny<User>()))
                .Callback((User model) => { emptyCollection.Remove(model); })
                .Returns((User model) => null);

            var userService = new UserService(context.Object);

            // Act
            await userService.RemoveAsync(testObject);

            // Assert
            Assert.Empty(emptyCollection);
        }
    }
}