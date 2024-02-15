using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ventas_pt.Entities.Config
{
    public class VentaConfig : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__3CD842E5A3F1C767");

            builder.Property(e => e.IdVenta).HasColumnName("ID_Venta");

            builder.Property(e => e.Fecha).HasColumnType("datetime");

            builder.Property(e => e.IdLocal).HasColumnName("ID_Local");

            builder.HasOne(d => d.IdLocalNavigation)
                .WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venta__ID_Local__571DF1D5");

        }
    }
}
