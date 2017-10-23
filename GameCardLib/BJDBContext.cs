using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class BJDBContext : DbContext
    {
        public BJDBContext() : base("DefaultConnection") { }
        public DbSet<Player> Players { get; set; }
    }
}
