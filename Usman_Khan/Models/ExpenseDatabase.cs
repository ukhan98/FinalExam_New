using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Usman_Khan.Models
{
    public class ExpenseDatabase : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FinalDatabase;Integrated Security=True;");
        }

    }
}
