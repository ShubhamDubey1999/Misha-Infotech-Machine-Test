using Microsoft.EntityFrameworkCore;
using MishaInfotech.API.Models;

namespace MishaInfotech.API.Data
{
    public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
        public DbSet<User> Users { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Cities> Cities { get; set; }
    }
}
