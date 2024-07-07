// Controllers/SensorPackController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorPackController : ControllerBase
    {
        private readonly SensorPackDAL _sensorPackDAL;

        public SensorPackController()
        {
            _sensorPackDAL = new SensorPackDAL();
        }

        [HttpPost]
        public IActionResult CreateSensorPack([FromBody] SensorPack sensorPack)
        {
            _sensorPackDAL.CreateSensorPack(sensorPack);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetSensorPackById(int id)
        {
            SensorPack sensorPack = _sensorPackDAL.GetSensorPackById(id);
            if (sensorPack == null)
            {
                return NotFound();
            }
            return Ok(sensorPack);
        }

        [HttpGet]
        public IActionResult GetAllSensorPacks()
        {
            List<SensorPack> sensorPacks = _sensorPackDAL.GetAllSensorPacks();
            return Ok(sensorPacks);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSensorPack(int id, [FromBody] SensorPack sensorPack)
        {
            sensorPack.ID = id;
            _sensorPackDAL.UpdateSensorPack(sensorPack);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSensorPack(int id)
        {
            _sensorPackDAL.DeleteSensorPack(id);
            return Ok();
        }
    }
}
