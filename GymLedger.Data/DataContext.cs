using GymLedger.Models;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Data
{
    public class DataContext : DbContext
    {
        // public DbSet<Model> TableName { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
    }
}
