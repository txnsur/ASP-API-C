// Controllers/ProveedorController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorDAL _proveedorDAL;

        public ProveedorController()
        {
            _proveedorDAL = new ProveedorDAL();
        }

        [HttpPost]
        public IActionResult CreateProveedor([FromBody] Proveedor proveedor)
        {
            _proveedorDAL.CreateProveedor(proveedor);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProveedorById(int id)
        {
            Proveedor proveedor = _proveedorDAL.GetProveedorById(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return Ok(proveedor);
        }

        [HttpGet]
        public IActionResult GetAllProveedores()
        {
            List<Proveedor> proveedores = _proveedorDAL.GetAllProveedores();
            return Ok(proveedores);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProveedor(int id, [FromBody] Proveedor proveedor)
        {
            proveedor.ID = id;
            _proveedorDAL.UpdateProveedor(proveedor);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProveedor(int id)
        {
            _proveedorDAL.DeleteProveedor(id);
            return Ok();
        }
    }
}
