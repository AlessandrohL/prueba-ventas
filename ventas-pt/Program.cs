
using ventas_pt.Entities;
using ventas_pt.Repository;

var ventaRepo = new VentaRepository();

var data = await ventaRepo.ObtenerDetallesDeVentas(30);

// El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).
var montoTotal = data.Sum(vd => vd.Total);
var cantidadVentas = data.Count;

Console.WriteLine($"Monto total: {montoTotal}");
Console.WriteLine($"Total de ventas únicas en los últimos 30 días: {cantidadVentas}");


// El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
var ventaMayor = data
    .OrderByDescending(vd => vd.Total)
    .Take(1)
    .ToArray()[0];

Console.WriteLine($"La venta con el monto más alto se realizo el {ventaMayor.Fecha.Day} a las {ventaMayor.Fecha.Hour}" +
    $" con el monto de {ventaMayor.Total}");


// Indicar cuál es el producto con mayor monto total de ventas. 
var productoMayorMontoTotalVentas = data
    .SelectMany(v => v.VentaDetalles)
    .GroupBy(vd => vd.IdProducto)
    .Select(g => new
    {
        IdProducto = g.Key,
        MontoTotal = g.Sum(vd => vd.TotalLinea)
    })
    .OrderByDescending(vd => vd.MontoTotal)
    .FirstOrDefault();

Console.WriteLine($"Id del producto con mayor monto total de ventas: " +
    $"{productoMayorMontoTotalVentas!.IdProducto} con el monto de {productoMayorMontoTotalVentas.MontoTotal}");


// Indicar el local con mayor monto de ventas.
var localMayorMontoDeVentas = data
    .GroupBy(v => v.IdLocal)
    .Select(g => new
    {
        IdLocal = g.Key,
        MontoTotal = g.Sum(v => v.Total)
    })
    .OrderByDescending(vd => vd.MontoTotal)
    .FirstOrDefault();

Console.WriteLine($"Local con mayor monto total de ventas: {localMayorMontoDeVentas?.IdLocal} " +
    $"con el monto de {localMayorMontoDeVentas?.MontoTotal}");


var marcaMayorMargenGanancia = data
    .SelectMany(v => v.VentaDetalles)
    .GroupBy(v => v.IdProductoNavigation.IdMarca)
    .Select(g => new
    {
        IdMarca = g.Key,
        NombreMarca = g.FirstOrDefault()?.IdProductoNavigation.IdMarcaNavigation.Nombre,
        MargenGanancia = g.Sum(p => ((p.PrecioUnitario - p.IdProductoNavigation.CostoUnitario) / p.PrecioUnitario) * 100)
    })
    .OrderByDescending(m => m.MargenGanancia)
    .FirstOrDefault();

Console.WriteLine($"Marca con mayor margen de ganancia: {marcaMayorMargenGanancia?.NombreMarca}" +
    $" con un margen del {marcaMayorMargenGanancia?.MargenGanancia}%");

var productosMasVendidoPorLocal = data
    .GroupBy(v => v.IdLocal)
    .Select(g => new
    {
        NombreLocal = g.FirstOrDefault()?.IdLocalNavigation.Nombre,
        Producto = g
            .SelectMany(v => v.VentaDetalles)
            .GroupBy(vd => vd.IdProducto)
            .Select(g => new
            {
                NombreProducto = g.FirstOrDefault()?.IdProductoNavigation.Nombre,
                CantidadTotalVendido = g.Sum(vd => vd.Cantidad)
            })
            .OrderByDescending(v => v.CantidadTotalVendido)
            .FirstOrDefault()
    });

foreach (var p in productosMasVendidoPorLocal)
{
    Console.WriteLine($"Local: {p.NombreLocal} | Producto más vendido: {p.Producto?.NombreProducto} " +
        $"| Cantidad Total vendido: {p.Producto?.CantidadTotalVendido}");
}











