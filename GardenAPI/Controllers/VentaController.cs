using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaDAL _ventaDAL;

        public VentaController(VentaDAL ventaDAL)
        {
            _ventaDAL = ventaDAL;
        }

        [HttpPost]
        public IActionResult CreateVenta([FromBody] Venta venta)
        {
            _ventaDAL.CreateVenta(venta);
            return Ok(venta); // Puedes devolver la venta creada si es relevante para el cliente
        }

        [HttpGet("{id}")]
        public IActionResult GetVentaById(int id)
        {
            Venta venta = _ventaDAL.GetVentaById(id);
            if (venta == null)
            {
                return NotFound();
            }
            return Ok(venta);
        }

        [HttpGet]
        public IActionResult GetAllVentas()
        {
            List<Venta> ventas = _ventaDAL.GetAllVentas();
            return Ok(ventas);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVenta(int id, [FromBody] Venta venta)
        {
            if (id != venta.ID)
            {
                return BadRequest("El ID proporcionado en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            _ventaDAL.UpdateVenta(venta);
            return Ok(venta);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVenta(int id)
        {
            _ventaDAL.DeleteVenta(id);
            return Ok();
        }
    }
}
