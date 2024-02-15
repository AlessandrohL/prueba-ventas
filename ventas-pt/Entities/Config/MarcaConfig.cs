using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ventas_pt.Entities.Config
{
    public class MarcaConfig : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.HasKey(e => e.IdMarca)
                    .HasName("PK__Marca__9B8F8DB2325A25B9");

            builder.ToTable("Marca");

            builder.Property(e => e.IdMarca).HasColumnName("ID_Marca");

            builder.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
