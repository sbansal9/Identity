using Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data
{ 
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {

        }

        public DbSet<Person> Person { get; set; }
        //public DbSet<SpGetProductByPriceGreaterThan1000> Products { get; set; }
        //public DbSet<SpGetProductByID> Product { get; set; }
    }
}
