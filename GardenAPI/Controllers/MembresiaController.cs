// Controllers/MembresiaController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembresiaController : ControllerBase
    {
        private readonly MembresiaDAL _membresiaDAL;

        public MembresiaController()
        {
            _membresiaDAL = new MembresiaDAL();
        }

        [HttpPost]
        public IActionResult CreateMembresia([FromBody] Membresia membresia)
        {
            _membresiaDAL.CreateMembresia(membresia);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetMembresiaById(int id)
        {
            Membresia membresia = _membresiaDAL.GetMembresiaById(id);
            if (membresia == null)
            {
                return NotFound();
            }
            return Ok(membresia);
        }

        [HttpGet]
        public IActionResult GetAllMembresias()
        {
            List<Membresia> membresias = _membresiaDAL.GetAllMembresias();
            return Ok(membresias);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMembresia(int id, [FromBody] Membresia membresia)
        {
            membresia.ID = id;
            _membresiaDAL.UpdateMembresia(membresia);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMembresia(int id)
        {
            _membresiaDAL.DeleteMembresia(id);
            return Ok();
        }
    }
}
