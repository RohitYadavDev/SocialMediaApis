using Microsoft.EntityFrameworkCore;
using SocialMediaApis.Models;

namespace SocialMediaApis.DBContext
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        //declare the model
        public DbSet<User> users { get; set; }  
    }
}
