using System;
using System.Collections.Generic;

namespace Proyecto_API_KevinRodriguezM.Models;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public DateTime Fecha { get; set; }

    public int Cantidad { get; set; }

    public int ProveedorId { get; set; }

    public int MotoId { get; set; }

    public virtual Moto Moto { get; set; } = null!;

    public virtual Proveedor Proveedor { get; set; } = null!;
}
