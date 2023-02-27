using BusinessLayer.Services;
using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hogwarts_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspirantController : Controller
    {
        private readonly ISchoolService _schoolService;

        public AspirantController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }
        // GET api/<Aspirant>/
        [HttpGet]
        public async Task<ActionResult<Aspirant>> GetAspirantsAsync()
        {

            var aspirants = await _schoolService.GetAspirantsAsync();
            if (aspirants == null)
            {
                return StatusCode(404, "No aspirants in database");
            }
            return Ok(aspirants);
        }

        // GET api/<Aspirant>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aspirant>> GetAspirantAsync(int id)
        {
            var aspirant = await _schoolService.GetAspirantAsync(id);
            if (aspirant == null)
            {
                return StatusCode(404, $"No aspirants found for aspirant id: {id}");
            }

            return Ok(aspirant);
        }

        // POST api/<AdmissionRequestController>
        [HttpPost]
        public async Task<ActionResult<Aspirant>> AddAspirantRequest(Aspirant aspirant)
        {
            Aspirant dbAspirant = await _schoolService.AddAspirantAsync(aspirant);

            if (dbAspirant == null)
            {
                return StatusCode(500, $"aspirant {aspirant.FirstName} {aspirant.LastName} could not be added.");
            }
            return StatusCode(201, dbAspirant);
        }

        // PUT api/<Aspirant>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Aspirant>> UpdateAspirantAsync(int id, Aspirant aspirant)
        {
            if (id != aspirant.AspirantId)
            {
                return BadRequest();
            }

            Aspirant dbAspirant = await _schoolService.UpdateAspirantAsync(aspirant);

            if (dbAspirant == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"admission request id: {aspirant.FirstName} {aspirant.LastName} could not be updated");
            }

            return dbAspirant;
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            (bool status, string message) = await _schoolService.DeleteAspirantAsync(id);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return Ok($"Aspirant id: {id} has was removed");
        }
    }
}
