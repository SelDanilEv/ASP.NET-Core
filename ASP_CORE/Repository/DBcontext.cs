using ASP_CORE.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE.Repository
{
    public class DBcontext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }
    }
}
