using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.EntityFrameworkCore;


namespace MotorBrakeTestApp.Services
{
    internal class BrakeDbContext : DbContext
    {
        public string DataSource { get; } = System.Windows.Forms.Application.StartupPath + "\\BrakeDb.db3";

        public DbSet<VoltageModels> VoltageModel { get; set; }
        public DbSet<OrderCounts> OrderIndex { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source = " + DataSource);
            base.OnConfiguring(optionsBuilder);
        }

    }
}
