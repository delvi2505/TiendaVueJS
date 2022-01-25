using System;
using System.Collections.Generic;

namespace VueStoreApi.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Carritos = new HashSet<Carrito>();
        }

        public string Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;

        public virtual ICollection<Carrito> Carritos { get; set; }
    }
}
