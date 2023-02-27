using BusinessLayer.Services;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hogwarts_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionRequestController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public AdmissionRequestController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // GET: api/<AdmissionRequestController>
        [HttpGet]
        public async Task<ActionResult<AdmissionRequest>> GetAdmissionRequestsAsync()
        {

            var admissionRequest = await _schoolService.GetAdmissionRequestsAsync();
            if (admissionRequest == null)
            {
                return StatusCode(404, "No admissions request in database");
            }
            return Ok(await _schoolService.GetAdmissionRequestsAsync());
        }

        // GET api/<AdmissionRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdmissionRequest>> GetAdmissionRequestAsync(int id) 
        {
            var admissionRequest = await _schoolService.GetAdmissionRequestAsync(id);

            if(admissionRequest == null)
            {
                return StatusCode(404, $"No admissions request found for admission request id: {id}");
            }

            return Ok(admissionRequest);
        }

        // POST api/<AdmissionRequestController>
        [HttpPost]
        public async Task<ActionResult<AdmissionRequest>> AddAdmissionRequest(AdmissionRequest admissionRequest)
        {
            AdmissionRequest dbAdmissionRequest = await _schoolService.AddAdmissionRequestAsync(admissionRequest);
            if(dbAdmissionRequest == null)
            {
                return StatusCode(500, $"admission request could not be added.");
            }
            return StatusCode(201, dbAdmissionRequest);
        }

        // PUT api/<AdmissionRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AdmissionRequest>> UpdateAdmissionRequestAsync(int id, AdmissionRequest admissionRequest)
        {
            if(id != admissionRequest.AdmissionRequestId)
            {
                return BadRequest();
            }

            AdmissionRequest dbAdmissionRequest = await _schoolService.UpdateAdmissionRequestAsync(admissionRequest);

            if( dbAdmissionRequest == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"admission request could not be updated");
            }

            return dbAdmissionRequest;
        }


        // DELETE api/<AdmissionRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdmissionRequest>> DeleteAdmissionRquestAsync(int id)
        {
            (bool status, string message) = await _schoolService.DeleteAdmissionRquestAsync(id);

            if(status == false)
            {
                return StatusCode(500, message);
            }

            return StatusCode(200, $"admission request id: {id} deleted");
            
        }
    }
}
