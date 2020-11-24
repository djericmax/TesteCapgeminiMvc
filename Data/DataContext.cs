using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteCapgeminiMvc.Models;

namespace TesteCapgeminiMvc.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Excel> Excels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=BcoExcel.sdb");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Excel>().ToTable("TbExcel").Property(e => e.DtEntrega).IsRequired();
            mb.Entity<Excel>().ToTable("TbExcel").Property(e => e.NomeProduto).IsRequired().HasMaxLength(50);
            mb.Entity<Excel>().ToTable("TbExcel").Property(e => e.Quantidade).IsRequired();
            mb.Entity<Excel>().ToTable("TbExcel").Property(e => e.ValorUnitario).IsRequired();
        }

        
    }
}
