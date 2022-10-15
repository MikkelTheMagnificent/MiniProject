using Microsoft.EntityFrameworkCore;
using shared.Model;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Collections.Generic;

namespace Data
{
    public class dbContext : DbContext
    {
        public DbSet<Post> posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();


        public dbContext (DbContextOptions<dbContext> options)
            : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
}
