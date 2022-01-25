using System;
using System.Collections.Generic;

namespace VueStoreApi.Models
{
    public partial class DetalleCarrito
    {
        public int Id { get; set; }
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }

        public virtual Carrito IdCarritoNavigation { get; set; } = null!;
        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
