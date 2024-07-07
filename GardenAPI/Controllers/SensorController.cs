// Controllers/SensorController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly SensorDAL _sensorDAL;

        public SensorController()
        {
            _sensorDAL = new SensorDAL();
        }

        [HttpPost]
        public IActionResult CreateSensor([FromBody] Sensor sensor)
        {
            _sensorDAL.CreateSensor(sensor);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetSensorById(int id)
        {
            Sensor sensor = _sensorDAL.GetSensorById(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return Ok(sensor);
        }

        [HttpGet]
        public IActionResult GetAllSensors()
        {
            List<Sensor> sensors = _sensorDAL.GetAllSensors();
            return Ok(sensors);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSensor(int id, [FromBody] Sensor sensor)
        {
            sensor.ID = id;
            _sensorDAL.UpdateSensor(sensor);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSensor(int id)
        {
            _sensorDAL.DeleteSensor(id);
            return Ok();
        }
    }
}
