// Controllers/PlantillaController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantillaController : ControllerBase
    {
        private readonly PlantillaDAL _plantillaDAL;

        public PlantillaController()
        {
            _plantillaDAL = new PlantillaDAL();
        }

        [HttpPost]
        public IActionResult CreatePlantilla([FromBody] Plantilla plantilla)
        {
            _plantillaDAL.CreatePlantilla(plantilla);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetPlantillaById(int id)
        {
            Plantilla plantilla = _plantillaDAL.GetPlantillaById(id);
            if (plantilla == null)
            {
                return NotFound();
            }
            return Ok(plantilla);
        }

        [HttpGet]
        public IActionResult GetAllPlantillas()
        {
            List<Plantilla> plantillas = _plantillaDAL.GetAllPlantillas();
            return Ok(plantillas);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlantilla(int id, [FromBody] Plantilla plantilla)
        {
            plantilla.ID = id;
            _plantillaDAL.UpdatePlantilla(plantilla);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlantilla(int id)
        {
            _plantillaDAL.DeletePlantilla(id);
            return Ok();
        }
    }
}
