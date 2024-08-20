using System;
using System.Collections.Generic;

namespace Proyecto_API_KevinRodriguezM.Models;

public partial class Venta
{
    public int VentaId { get; set; }

    public DateTime Fecha { get; set; }

    public int MontoTotal { get; set; }

    public int ClienteId { get; set; }

    public int EmpleadoId { get; set; }

    public int MotoId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Moto Moto { get; set; } = null!;
}
