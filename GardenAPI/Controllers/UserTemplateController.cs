// Controllers/UserTemplateController.cs
using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Collections.Generic;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTemplateController : ControllerBase
    {
        private readonly UserTemplateDAL _userTemplateDAL;

        public UserTemplateController()
        {
            _userTemplateDAL = new UserTemplateDAL();
        }

        [HttpPost]
        public IActionResult AddUserTemplate([FromBody] UserTemplate userTemplate)
        {
            _userTemplateDAL.AddUserTemplate(userTemplate.UserID, userTemplate.TemplateID, userTemplate.GardenID);
            return Ok();
        }

        [HttpGet("{userId}/{templateId}")]
        public IActionResult GetUserTemplate(int userId, int templateId)
        {
            UserTemplate userTemplate = _userTemplateDAL.GetUserTemplate(userId, templateId);
            if (userTemplate == null)
            {
                return NotFound();
            }
            return Ok(userTemplate);
        }

        [HttpGet]
        public IActionResult GetAllUserTemplates()
        {
            List<UserTemplate> userTemplates = _userTemplateDAL.GetAllUserTemplates();
            return Ok(userTemplates);
        }

        [HttpPut]
        public IActionResult UpdateUserTemplate([FromBody] UserTemplate userTemplate)
        {
            _userTemplateDAL.UpdateUserTemplate(userTemplate);
            return Ok();
        }

        [HttpDelete("{userId}/{templateId}")]
        public IActionResult DeleteUserTemplate(int userId, int templateId)
        {
            _userTemplateDAL.DeleteUserTemplate(userId, templateId);
            return Ok();
        }
    }
}
