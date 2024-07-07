// Controllers/LecturaController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturaController : ControllerBase
    {
        private readonly LecturaDAL _lecturaDAL;

        public LecturaController()
        {
            _lecturaDAL = new LecturaDAL();
        }

        [HttpPost]
        public IActionResult CreateLectura([FromBody] Lectura lectura)
        {
            _lecturaDAL.CreateLectura(lectura);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetLecturaById(int id)
        {
            Lectura lectura = _lecturaDAL.GetLecturaById(id);
            if (lectura == null)
            {
                return NotFound();
            }
            return Ok(lectura);
        }

        [HttpGet]
        public IActionResult GetAllLecturas()
        {
            List<Lectura> lecturas = _lecturaDAL.GetAllLecturas();
            return Ok(lecturas);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLectura(int id, [FromBody] Lectura lectura)
        {
            lectura.ID = id;
            _lecturaDAL.UpdateLectura(lectura);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLectura(int id)
        {
            _lecturaDAL.DeleteLectura(id);
            return Ok();
        }
    }
}
