using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ventas_pt.Entities.Config
{
    public partial class VentaDetalleConfig : IEntityTypeConfiguration<VentaDetalle>
    {
        public void Configure(EntityTypeBuilder<VentaDetalle> builder)
        {
            builder.HasKey(e => e.IdVentaDetalle)
                .HasName("PK__VentaDet__2F0CE38B52091CC3");

            builder.ToTable("VentaDetalle");

            builder.Property(e => e.IdVentaDetalle).HasColumnName("ID_VentaDetalle");

            builder.Property(e => e.IdProducto).HasColumnName("ID_Producto");

            builder.Property(e => e.IdVenta).HasColumnName("ID_Venta");

            builder.Property(e => e.PrecioUnitario).HasColumnName("Precio_Unitario");

            builder.HasOne(d => d.IdProductoNavigation)
                .WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VentaDeta__ID_Pr__5DCAEF64");

            builder.HasOne(d => d.IdVentaNavigation)
                .WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VentaDeta__ID_Ve__5CD6CB2B");
        }
    }
}
