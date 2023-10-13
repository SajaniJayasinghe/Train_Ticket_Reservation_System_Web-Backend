using Microsoft.AspNetCore.Mvc;
using Train_Reservation_System.Helpers;
using Train_Reservation_System.Models.TravelAgent;
using Train_Reservation_System.Models.Travelers;
using Train_Reservation_System.Services.TravelAgents;
using Train_Reservation_System.Services.Travelers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Train_Reservation_System.Controllers.Travelers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelerController : ControllerBase
    {
        private readonly ITravelerService travelerService;

        public TravelerController(ITravelerService travelerService)
        {
            this.travelerService = travelerService;
        }
        // GET: api/<TravelerController>
        [HttpGet]
        public ActionResult<List<Traveler>> Get()
        {
            try
            {
                var travelagents = travelerService.Get();
                return Ok(new ApiResponse(true, 200, "Success", travelagents));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        // GET api/<TravelerController>/5
        [HttpGet("{nic}")]
        public ActionResult<Traveler> Get(string nic)
        {
            try
            {
                var traveler = travelerService.Get(nic);
                if (traveler == null)
                {
                    return NotFound(new ApiResponse(false, 404, $"Traveler with NIC = {nic} not found", null));
                }
                return Ok(new ApiResponse(true, 200, "Success", traveler));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        // POST api/<TravelerController>
        [HttpPost]
        public ActionResult<ApiResponse> CreateTravveler([FromBody] Traveler traveler)
        {
            try
            {
                travelerService.Create(traveler);
                return new ApiResponse(true, 201, "Traveler created successfully", traveler);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, 500, "Error creating Traveler", ex.Message);
            }
        }

        // PUT api/<TravelerController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Traveler traveler)
        {
            var existingTraveler = travelerService.Get(id);

            if (existingTraveler == null)
            {
                return NotFound(new ApiResponse(false, 401, $"Traveler with NIC = {id} not found", null));
            }

            travelerService.Update(id, traveler);

            return Ok(new ApiResponse(true, 200, "Traveler updated successfully", existingTraveler));
        }
        // DELETE api/<TravelerController>/5
        [HttpDelete("{nic}")]
        public ActionResult Delete(string nic)
        {
            var traveler = travelerService.Get(nic);
            if (traveler == null)
            {
                return NotFound(new ApiResponse(false, 401, $"Traveler with NIC = {nic} not found", null));
            }
            travelerService.Remove(traveler.NIC);
            return Ok(new ApiResponse(true, 200, "Success", traveler));
        }

        // POST api/<TravelerController>/login
        [HttpPost("login")]
        public ActionResult<Traveler> Login([FromBody] TravelerLoginRequest request)
        {
            var traveler = travelerService.Login(request.NIC, request.Password);
            if (traveler == null)
            {
                return NotFound(new ApiResponse(false, 401, "Invalid NIC or Password", null));
            }
            return Ok(new ApiResponse(true, 200, "Success", new { Traveler = traveler }));
        }

        [HttpPost("activate/{nic}")]
        public IActionResult Activate(string nic)
        {
            var traveler = travelerService.Get(nic);
            if (traveler == null)
            {
                return Ok(new ApiResponse(false, 404, $"Traveler with NIC = {nic} not found", null));
            }

            travelerService.Activate(nic);
            return Ok(new ApiResponse(true, 200, $"Traveler with NIC = {nic} activated", null));
        }

        // POST api/<TravelerController>/deactivate/{nic}

        [HttpPost("deactivate/{nic}")]
        public IActionResult Deactivate(string nic)
        {
            var traveler = travelerService.Get(nic);
            if (traveler == null)
            {
                return Ok(new ApiResponse(false, 404, $"Traveler with NIC = {nic} not found", null));
            }

            travelerService.Deactivate(nic);
            return Ok(new ApiResponse(true, 200, $"Traveler with NIC = {nic} dectivated", null));
        }
    }
}
