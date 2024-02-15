using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ventas_pt.Data;
using ventas_pt.Entities;

namespace ventas_pt.Repository
{
    public class VentaRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
            
        public VentaRepository()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
        }


        public async Task<List<VentaDetalle>> ObtenerDetallesDeVentas(int daysLimit)
        {
            var fechaLimite = DateTime.Today.AddDays(-30);

            using (var context = new ApplicationDbContext(_options))
            {
                var detallesVentas = await context.VentaDetalles
                    .Include(d => d.IdVentaNavigation)
                        .ThenInclude(v => v.IdLocalNavigation)
                    .Include(p => p.IdProductoNavigation)
                        .ThenInclude(p => p.IdMarcaNavigation)
                    .Where(d => d.IdVentaNavigation.Fecha >= fechaLimite)
                    .ToListAsync();

                return detallesVentas;
                    //.GroupBy(d => d.IdVenta)
                    //.Select(g => new
                    //{
                    //   MontoTotal = g.Sum(d => d.TotalLinea)
                    //});
            }

        }
    }
}
