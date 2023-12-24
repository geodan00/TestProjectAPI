using Microsoft.AspNetCore.Mvc;
using TestGeodanApi.Interfaces;
using TestGeodanApi.Models;
using TestGeodanApi.Services;

namespace TestGeodanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsers sUsers;
        public UsersController(IUsers iUsers)
        {
            sUsers = iUsers;
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult<Users>> LogIn(Users user)
        {
            try
            {
                Users? u = sUsers.LogIn(user);
                if (u == null)
                {
                    return NotFound();
                }

                return u;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
