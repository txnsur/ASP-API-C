// Controllers/UserMembershipController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMembershipController : ControllerBase
    {
        private readonly UserMembershipDAL _userMembershipDAL;

        public UserMembershipController()
        {
            _userMembershipDAL = new UserMembershipDAL();
        }

        [HttpPost]
        public IActionResult CreateUserMembership([FromBody] UserMembership userMembership)
        {
            _userMembershipDAL.CreateUserMembership(userMembership);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserMembershipById(int id)
        {
            UserMembership userMembership = _userMembershipDAL.GetUserMembershipById(id);
            if (userMembership == null)
            {
                return NotFound();
            }
            return Ok(userMembership);
        }

        [HttpGet]
        public IActionResult GetAllUserMemberships()
        {
            List<UserMembership> userMemberships = _userMembershipDAL.GetAllUserMemberships();
            return Ok(userMemberships);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserMembership(int id, [FromBody] UserMembership userMembership)
        {
            userMembership.ID = id;
            _userMembershipDAL.UpdateUserMembership(userMembership);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserMembership(int id)
        {
            _userMembershipDAL.DeleteUserMembership(id);
            return Ok();
        }
    }
}
