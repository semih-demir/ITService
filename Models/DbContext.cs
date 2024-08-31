using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ITService.Models
{
    public class ITServiceContext : DbContext
    {
        public ITServiceContext(DbContextOptions<ITServiceContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
