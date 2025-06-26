using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lozovoi_Lab4_Diagrammer
{
    public class DiagramDbContext : DbContext
    {
        public DbSet<DbPrimitive> dbPrimitives { get; set; }
        public DbSet<DbLink> dbLinks { get; set; }

        private readonly string dbPath = "";

        public DiagramDbContext(string _dbPath)
        {
            this.dbPath = _dbPath;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + dbPath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbLink>()
                        .HasOne(m => m.linker)
                        .WithMany(t => t.linkers)
                        .HasForeignKey(m => m.linkerId)
                        .IsRequired();

            modelBuilder.Entity<DbLink>()
                        .HasOne(m => m.linkee)
                        .WithMany(t => t.linkees)
                        .HasForeignKey(m => m.linkeeId)
                        .IsRequired();
            modelBuilder.Entity<DbLink>()
                        .HasKey(c => new { c.linkerId, c.linkeeId });
        }
    }

    [Table("Primitive")]
    public class DbPrimitive
    {
        public int Id { get; set; }
        public string? label { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string type { get; set; }

        public virtual ICollection<DbLink> linkers { get; set; }
        public virtual ICollection<DbLink> linkees { get; set; }
    }

    [Table("Link")]
    public class DbLink
    {
        public int linkerId { get; set; }
        public int linkeeId { get; set; }


        public virtual DbPrimitive linker { get; set; }
        public virtual DbPrimitive linkee { get; set; }
    }
}
