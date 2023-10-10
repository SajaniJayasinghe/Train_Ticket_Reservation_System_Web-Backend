using Microsoft.AspNetCore.Mvc;
using Train_Reservation_System.Models.Travelers;
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
            return travelerService.Get();
        }

        // GET api/<TravelerController>/5
        [HttpGet("{nic}")]
        public ActionResult<Traveler> Get(string nic)
        {
            var traveler = travelerService.Get(nic);
            if (traveler == null)
            {
                return NotFound($"Traveler with NIC = {nic} not found");
            }
            return traveler;
        }

        // POST api/<TravelerController>
        [HttpPost]
        public ActionResult<Traveler> Post([FromBody] Traveler traveler)
        {
            travelerService.Create(traveler);
            return CreatedAtAction(nameof(Get), new { nic = traveler.NIC }, traveler);
        }

        // PUT api/<TravelerController>/5
        [HttpPut("{nic}")]
        public ActionResult Put(string nic, [FromBody] Traveler traveler)
        {
            var existingTraveler = travelerService.Get(nic);

            if (existingTraveler == null)
            {
                return NotFound($"Traveler with NIC = {nic} not found");
            }
            travelerService.Update(nic, traveler);
            return NoContent();
        }

        // DELETE api/<TravelerController>/5
        [HttpDelete("{nic}")]
        public ActionResult Delete(string nic)
        {
            var traveler = travelerService.Get(nic);
            if (traveler == null)
            {
                return NotFound($"Traveler with NIC = {nic} not found");
            }
            travelerService.Remove(traveler.NIC);
            return Ok($"Traveler with NIC = {nic} deleted");
        }

        // POST api/<TravelerController>/login
        [HttpPost("login")]
        public ActionResult<Traveler> Login([FromBody] TravelerLoginRequest request)
        {
            var traveler = travelerService.Login(request.NIC, request.Password);
            if (traveler == null)
            {
                return NotFound("Invalid NIC or password");
            }
            return Ok(traveler);
        }

        // POST api/<TravelerController>/activate/{nic}
        [HttpPost("activate/{nic}")]
        public IActionResult Activate(string nic)
        {
            var traveler = travelerService.Get(nic);
            if (traveler == null)
            {
                return NotFound($"Traveler with NIC = {nic} not found");
            }

            travelerService.Activate(nic);
            return Ok($"Traveler with NIC = {nic} activated");
        }

        // POST api/<TravelerController>/deactivate/{nic}

        [HttpPost("deactivate/{nic}")]
        public IActionResult Deactivate(string nic)
        {
            var traveler = travelerService.Get(nic);
            if (traveler == null)
            {
                return NotFound($"Traveler with NIC = {nic} not found");
            }

            travelerService.Deactivate(nic);
            return Ok($"Traveler with NIC = {nic} deactivated");
        }
    }
}
