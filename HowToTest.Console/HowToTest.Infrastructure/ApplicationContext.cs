using HowToTest.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HowToTest.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
    }
}
