USE Prueba;

-- El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).
SELECT SUM(v.Total) AS Monto_Total, 
       COUNT(v.ID_Venta) AS Total_Ventas
FROM Venta AS v
WHERE v.Fecha >= DATEADD(DAY, -30, GETDATE());


-- El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto)
SELECT TOP 1 
       DAY(v.Fecha) AS Dia,
       DATEPART(HOUR, v.Fecha) AS Hora,
       v.Total AS Monto
FROM Venta AS v
ORDER BY Monto DESC;


-- Indicar cuál es el producto con mayor monto total de ventas. 
SELECT TOP 1
       vd.ID_Producto,
       p.Nombre,
       SUM(vd.TotalLinea) AS Monto_Total_Ventas
FROM VentaDetalle AS vd
INNER JOIN Producto AS p
ON vd.ID_Producto = p.ID_Producto
GROUP BY vd.ID_Producto, p.Nombre
ORDER BY Monto_Total_Ventas DESC;

-- Indicar el local con mayor monto de ventas.
SELECT TOP 1
       l.ID_Local,
       l.Nombre,
       SUM(v.Total) AS Monto_Ventas
FROM Venta as v
INNER JOIN [Local] AS l ON v.ID_Local = l.ID_Local
GROUP BY l.ID_Local, l.Nombre
ORDER BY Monto_Ventas DESC;


-- •	¿Cuál es la marca con mayor margen de ganancias?
SELECT TOP 1
    m.ID_Marca,
    SUM(((vd.Precio_Unitario - p.Costo_Unitario) / vd.Precio_Unitario) * 100) AS MargenGanancia
FROM VentaDetalle AS vd
INNER JOIN Producto AS p ON vd.ID_Producto = p.ID_Producto
INNER JOIN Marca AS m ON p.ID_Marca = m.ID_Marca
GROUP BY m.ID_Marca
ORDER BY MargenGanancia DESC;

-- •	¿Cómo obtendrías cuál es el producto que más se vende en cada local?

WITH VentaDetallesPorLocal AS (
    SELECT
        v.ID_Local,
        p.ID_Producto,
        p.Nombre AS NombreProducto,
        SUM(vd.Cantidad) AS CantidadTotalVendida,
        ROW_NUMBER() OVER(PARTITION BY v.ID_Local ORDER BY SUM(vd.Cantidad) DESC) AS RN
    FROM
        VentaDetalle AS vd
    INNER JOIN
        Venta AS v ON v.ID_Venta = vd.ID_Venta
    INNER JOIN
        Producto AS p ON vd.ID_Producto = p.ID_Producto
    GROUP BY
        v.ID_Local, p.ID_Producto, p.Nombre
)
SELECT
    ID_Local,
    ID_Producto,
    NombreProducto,
    CantidadTotalVendida
FROM
    VentaDetallesPorLocal
WHERE
    RN = 1;


