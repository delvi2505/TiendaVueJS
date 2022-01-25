using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VueStoreApi.Models;
using VueStoreApi.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopingCarController : ControllerBase
    {
        private readonly VueStoreDBContext _context;

        public ShopingCarController(VueStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/<ShopingCarController>
        [HttpGet("/GetCar/{id}")]
        public ActionResult Get(string id)
        {
            Carrito carrito = _context.Carritos.First(x => x.IdCliente == id);
            return Ok(carrito);
        }

        [HttpGet("/GetProductFromCar/{id}")]
        public ActionResult GetProductFromCar(string id)
        {
            Carrito carrito = _context.Carritos.First(x => x.IdCliente == id);
            List<DetalleCarrito> detalleCarritos = _context.DetalleCarritos.Where(x => x.IdCarrito == carrito.Id).ToList();
            List<ProductosJson> productos = new List<ProductosJson>();

            foreach (DetalleCarrito detalleCarrito in detalleCarritos)
            {
                var productoDB = _context.Productos.First(x => x.Id == detalleCarrito.IdProducto);
                productos.Add(new ProductosJson()
                {
                    Id = productoDB.Id,
                    Nombre = productoDB.Nombre,
                    UrlImagen = productoDB.UrlImagen,
                    Stock = productoDB.Stock,
                    Precio = productoDB.Precio,
                    IdDetalle = detalleCarrito.Id
                });
            }
            return Ok(JsonSerializer.Serialize(productos));
        }

        // POST api/<ShopingCarController>
        [HttpPost("/AddToCar")]
        public ActionResult Post([FromBody] ClienteProducto data)
        {
            Cliente cliente = _context.Clientes.First(x => x.Id == data.IdCliente);
            if (cliente == null)
                return BadRequest("Objeto cliente es nulo");

            Carrito carrito = _context.Carritos.First(x => x.IdCliente == cliente.Id);
            if (carrito == null)
                return BadRequest("Objeto carrito es nulo");

            var product = _context.Productos.First(x => x.Id == data.IdProducto);
            if (product == null)
                return BadRequest("Objeto producto es nulo");

            if (product.Stock <= _context.DetalleCarritos.Where(x => x.IdCarrito == carrito.Id && x.IdProducto == product.Id).Count())
                return Ok("No puede agregar más veces este articulo");

            if (product.Stock <= 0)
                return BadRequest("El artículo está agotado");

            DetalleCarrito detalleCarrito = new DetalleCarrito()
            {
                IdCarrito = carrito.Id,
                IdProducto = product.Id
            };
            _context.DetalleCarritos.Add(detalleCarrito);
            carrito.TotalApagar += product.Precio;
            carrito.CantProductos += 1;

            _context.SaveChanges();



            return Ok("OK");

        }

        [HttpPost("/Purchase/{id}")]
        public ActionResult Purchase(string id, [FromBody] List<ProductosJson> data)
        {
            Carrito carrito = _context.Carritos.First(x => x.IdCliente == id);
            if (carrito == null)
                return BadRequest("Objeto carrito es nulo");


            if (data.Count() <= 0)
                return Ok("La lista de productos está vacía");

            List<DetalleCarrito> detalleCarritos = _context.DetalleCarritos.Where(x => x.IdCarrito == carrito.Id).ToList();
            foreach(DetalleCarrito producto in detalleCarritos)
            {
                var productDB = _context.Productos.First(x => x.Id == producto.IdProducto);

                _context.DetalleCarritos.Remove(producto);
                productDB.Stock -= 1;

            }
            carrito.TotalApagar = 0;
            carrito.CantProductos = 0;
            _context.SaveChanges();

            return Ok("OK");

        }

        // PUT api/<ShopingCarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShopingCarController>/5
        [HttpDelete("/DeleteFromCar/{id}")]
        public ActionResult Delete(int id)
        {
            var detalleCar = _context.DetalleCarritos.First(x => x.Id == id);
            var producto = _context.Productos.First(x => x.Id == detalleCar.IdProducto);
            var carrito = _context.Carritos.First(x => x.Id == detalleCar.IdCarrito);

            carrito.TotalApagar -= producto.Precio;
            if (carrito.TotalApagar < 0)
                carrito.TotalApagar = 0;

            carrito.CantProductos -= 1;

            _context.DetalleCarritos.Remove(detalleCar);
            _context.SaveChanges();
            return Ok("OK");


        }
    }
}
