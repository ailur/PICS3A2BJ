using GameCardLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class BJDBContext : DbContext
    {
        public BJDBContext() : base("DefaultConnection") { }
        public DbSet<Player> Players { get; set; }
    }
}
