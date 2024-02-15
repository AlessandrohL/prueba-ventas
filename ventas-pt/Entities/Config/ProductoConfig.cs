using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ventas_pt.Entities.Config
{
    public class ProductoConfig : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__9B4120E21FBD1C85");

            builder.ToTable("Producto");

            builder.Property(e => e.IdProducto).HasColumnName("ID_Producto");

            builder.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.CostoUnitario).HasColumnName("Costo_Unitario");

            builder.Property(e => e.IdMarca).HasColumnName("ID_Marca");

            builder.Property(e => e.Modelo)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(d => d.IdMarcaNavigation)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__ID_Mar__52593CB8");
        }
    }
}
