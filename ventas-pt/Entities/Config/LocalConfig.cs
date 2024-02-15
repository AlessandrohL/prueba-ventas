using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ventas_pt.Entities.Config
{
    public class LocalConfig : IEntityTypeConfiguration<Local>
    {
        public void Configure(EntityTypeBuilder<Local> builder)
        {
            builder.HasKey(e => e.IdLocal)
                    .HasName("PK__Local__3E34B29DF6370FC0");

            builder.ToTable("Local");

            builder.Property(e => e.IdLocal).HasColumnName("ID_Local");

            builder.Property(e => e.Direccion)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
