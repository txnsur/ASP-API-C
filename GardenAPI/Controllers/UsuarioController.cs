// Controllers/UsuarioController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDAL _usuarioDAL;

        public UsuarioController()
        {
            _usuarioDAL = new UsuarioDAL();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] Usuario user)
        {
            _usuarioDAL.CreateUser(user);
            return Ok();
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            Usuario user = _usuarioDAL.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<Usuario> users = _usuarioDAL.GetAllUsers();
            return Ok(users);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] Usuario user)
        {
            _usuarioDAL.UpdateUser(user);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            _usuarioDAL.DeleteUser(userId);
            return Ok();
        }
    }
}
