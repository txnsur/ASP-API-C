// Controllers/CompraController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly CompraDAL _compraDAL;

        public CompraController()
        {
            _compraDAL = new CompraDAL();
        }

        [HttpPost]
        public IActionResult CreateCompra([FromBody] Compra compra)
        {
            _compraDAL.CreateCompra(compra);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetCompraById(int id)
        {
            Compra compra = _compraDAL.GetCompraById(id);
            if (compra == null)
            {
                return NotFound();
            }
            return Ok(compra);
        }

        [HttpGet]
        public IActionResult GetAllCompras()
        {
            List<Compra> compras = _compraDAL.GetAllCompras();
            return Ok(compras);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompra(int id, [FromBody] Compra compra)
        {
            compra.ID = id;
            _compraDAL.UpdateCompra(compra);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompra(int id)
        {
            _compraDAL.DeleteCompra(id);
            return Ok();
        }
    }
}
