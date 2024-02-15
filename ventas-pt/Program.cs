
using ventas_pt.Entities;
using ventas_pt.Repository;

var ventaRepo = new VentaRepository();

var data = await ventaRepo.ObtenerDetallesDeVentas(30);

var montoTotal = data.Sum(vd => vd.TotalLinea);
var cantidadVentas = data
    .Select(vd => vd.IdVenta)
    .Distinct()
    .Count();

var ventaMayor = data
    .OrderByDescending(vd => vd.TotalLinea)
    .Take(1)
    .ToArray()[0];

var ventaMayorFecha = ventaMayor.IdVentaNavigation.Fecha;

var productoMayorMontoTotalVentas = data
    .GroupBy(vd => vd.IdProducto)
    .Select(g => new
    {
        IdProducto = g.Key,
        MontoTotal = g.Sum(vd => vd.TotalLinea)
    })
    .OrderByDescending(vd => vd.MontoTotal)
    .FirstOrDefault();

var localMayorMontoDeVentas = data
    .GroupBy(vd => vd.IdVentaNavigation.IdLocalNavigation)
    .Select(g => new
    {
        IdLocal = g.Key,
        MontoTotal = g.Sum(vd => vd.TotalLinea)
    })
    .OrderByDescending(vd => vd.MontoTotal)
    .FirstOrDefault();

var marcaMayorMargenGanancia = data
    .GroupBy(vd => vd.IdProductoNavigation.IdMarca)
    .Select(g => new
    {
        IdMarca = g.Key,
        NombreMarca = g.FirstOrDefault()?.IdProductoNavigation.IdMarcaNavigation.Nombre,
        TotalMontoVentas = g.Sum(vd => vd.TotalLinea),
        CostoTotal = g.Sum(vd => vd.IdProductoNavigation.CostoUnitario * vd.Cantidad)
    })
    .Select(g => new
    {
        g.IdMarca,
        g.NombreMarca,
        MargenGanancia = ((g.TotalMontoVentas - g.CostoTotal) / g.TotalMontoVentas) * 100
    })
    .OrderByDescending(m => m.MargenGanancia)
    .FirstOrDefault();

var productosMasVendidoPorLocal = data
    .GroupBy(vd => vd.IdVentaNavigation.IdLocal)
    .Select(g => new
    {
        NombreLocal = g.FirstOrDefault()?.IdVentaNavigation.IdLocalNavigation.Nombre,
        Producto = g
            .GroupBy(vd => vd.IdProducto)
            .Select(g => new
            {
                NombreProducto = g.FirstOrDefault()?.IdProductoNavigation.Nombre,
                CantidadTotalVendido = g.Sum(vd => vd.Cantidad)
            })
            .OrderByDescending(v => v.CantidadTotalVendido)
            .FirstOrDefault()
    });



Console.WriteLine($"Monto total: {montoTotal}");
Console.WriteLine($"Total de ventas únicas en los últimos 30 días: {cantidadVentas}");

Console.WriteLine($"La venta con el monto más alto se realizo el {ventaMayorFecha.Day} a las {ventaMayorFecha.Hour}" +
    $" con el monto de {ventaMayor.TotalLinea}");

Console.WriteLine($"Id del producto con mayor monto total de ventas: {productoMayorMontoTotalVentas!.IdProducto}");

Console.WriteLine($"Marca con mayor margen de ganancia: {marcaMayorMargenGanancia?.NombreMarca}" +
    $" con un margen del {marcaMayorMargenGanancia?.MargenGanancia}%");

foreach (var p in productosMasVendidoPorLocal)
{
    Console.WriteLine($"Local: {p.NombreLocal} | Producto más vendido: {p.Producto?.NombreProducto}");
}




