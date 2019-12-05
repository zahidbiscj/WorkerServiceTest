using BitcoinCurrentPrice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice
{
    public class BitcoinContext : DbContext
    {
        private string _connectionString;

        public BitcoinContext()
        {
            _connectionString = "Server=DESKTOP-1CNTA40; Database = Test; User Id = zahid; Password = 123;";
        }
        public BitcoinContext(string connectionString)
        {
            _connectionString = "Server=DESKTOP-1CNTA40; Database = Test; User Id = zahid; Password = 123;";
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public DbSet<Bpi> Bpis { get; set; }
        public DbSet<EUR> EURs { get; set; }
        public DbSet<GBP> GBPs { get; set; }
        public DbSet<RootObject> RootObjects { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<USD> USDs { get; set; }
    }
}

