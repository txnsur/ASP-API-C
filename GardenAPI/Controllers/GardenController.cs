// Controllers/GardenController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GardenController : ControllerBase
    {
        private readonly GardenDAL _gardenDAL;

        public GardenController()
        {
            _gardenDAL = new GardenDAL();
        }

        [HttpPost]
        public IActionResult CreateGarden([FromBody] Garden garden)
        {
            _gardenDAL.CreateGarden(garden);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetGardenById(int id)
        {
            Garden garden = _gardenDAL.GetGardenById(id);
            if (garden == null)
            {
                return NotFound();
            }
            return Ok(garden);
        }

        [HttpGet]
        public IActionResult GetAllGardens()
        {
            List<Garden> gardens = _gardenDAL.GetAllGardens();
            return Ok(gardens);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGarden(int id, [FromBody] Garden garden)
        {
            garden.ID = id;
            _gardenDAL.UpdateGarden(garden);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGarden(int id)
        {
            _gardenDAL.DeleteGarden(id);
            return Ok();
        }
    }
}
