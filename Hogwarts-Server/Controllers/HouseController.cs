using BusinessLayer.Services;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hogwarts_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public HouseController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // GET: api/<HouseController>
        [HttpGet]
        public async Task<ActionResult<House>> GetHousesAsync()
        {
            var houses = await _schoolService.GetHousesAsync();
            if (houses == null)
            {
                return StatusCode(404, "No houses in database");
            }
            return Ok(houses);
        }

        // GET api/<HouseController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<House>> GetHouseAsync(int id)
        {
            var house = await _schoolService.GetHouseAsync(id);
            if (house == null)
            {
                return StatusCode(404, $"No house found for house id: {id}");
            }
            return Ok(house);
        }
    }
}
