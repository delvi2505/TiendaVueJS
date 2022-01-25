using System;
using System.Collections.Generic;

namespace VueStoreApi.Models
{
    public partial class Carrito
    {
        public Carrito()
        {
            DetalleCarritos = new HashSet<DetalleCarrito>();
        }

        public int Id { get; set; }
        public string IdCliente { get; set; } = null!;
        public int CantProductos { get; set; }
        public decimal TotalApagar { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; }
    }
}
