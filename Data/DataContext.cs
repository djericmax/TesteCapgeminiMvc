using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TesteCapgeminiMvc.Models;

namespace TesteCapgeminiMvc.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Excel> Excel { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=BcoExcel.sdb");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //mb.Entity<Excel>().HasKey(ToTable("TbExcel").Property(e => e.Id));
            mb.Entity<Excel>().ToTable("TbExcel").Property(e => e.DtEntrega).IsRequired();
            mb.Entity<Excel>().ToTable("TbExcel").Property(e => e.NomeProduto).IsRequired().HasMaxLength(50);
            mb.Entity<Excel>().ToTable("TbExcel").Property(e => e.Quantidade).IsRequired();
            mb.Entity<Excel>().ToTable("TbExcel").Property(e => e.ValorUnitario).IsRequired();
        }
    }
}
