using System;
using System.Collections.Generic;

namespace VueStoreApi.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCarritos = new HashSet<DetalleCarrito>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string UrlImagen { get; set; } = null!;
        public int Stock { get; set; }
        public decimal Precio { get; set; }

        public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; }
    }
}
