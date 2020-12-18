using HowToTest.Infrastructure.Tests.TestsUtilities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HowToTest.Infrastructure.Tests.Extensions
{
    public static class DbSetMockExtension
    {
        public static void SetupDbSet<T>(this Mock<DbSet<T>> dbSetMock, IQueryable<T> data)
            where T : class
        {
            dbSetMock.As<IAsyncEnumerable<T>>()
                   .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                   .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

            dbSetMock.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(data.Provider));

            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}