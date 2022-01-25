namespace VueStoreApi.Models.ViewModels
{
    public class ProductosJson
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string UrlImagen { get; set; } = null!;
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public int IdDetalle { get; set; }
    }
}
