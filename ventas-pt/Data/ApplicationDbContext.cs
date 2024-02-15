using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ventas_pt.Entities;
using ventas_pt.Entities.Config;

namespace ventas_pt.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly string _connStr;

        public virtual DbSet<Local> Locals { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<VentaDetalle> VentaDetalles { get; set; } = null!;
        public virtual DbSet<Venta> Venta { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        {
            _connStr = new ConnectionConfig().GetConnectionString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductoConfig());
            modelBuilder.ApplyConfiguration(new MarcaConfig());
            modelBuilder.ApplyConfiguration(new LocalConfig());
            modelBuilder.ApplyConfiguration(new VentaConfig());
            modelBuilder.ApplyConfiguration(new VentaDetalleConfig());
        }
    }
}
