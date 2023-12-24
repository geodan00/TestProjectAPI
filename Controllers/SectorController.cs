using Microsoft.AspNetCore.Mvc;
using TestGeodanApi.Interfaces;
using TestGeodanApi.Models;
using TestGeodanApi.Services;

namespace TestGeodanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : Controller
    {
        private readonly ISector sSector;

        public SectorController(ISector iSector)
        {
            sSector = iSector;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sector>>> GetAllSector()
        {
            try
            {
                List<Sector>? sectors = sSector.GetSectors();
                if (sectors == null)
                {
                    return NotFound();
                }

                return sectors;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
