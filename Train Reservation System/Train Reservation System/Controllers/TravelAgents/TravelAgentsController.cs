using Microsoft.AspNetCore.Mvc;
using Train_Reservation_System.Models.TravelAgent;
using Train_Reservation_System.Models.TravelAgents;
using Train_Reservation_System.Services.TravelAgents;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Train_Reservation_System.Controllers.TravelAgents
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelAgentsController : ControllerBase
    {
        private readonly ITravelAgentService travelAgentService;

        public TravelAgentsController(ITravelAgentService travelAgentService)
        {
            this.travelAgentService = travelAgentService;
        }
        // GET: api/<TravelAgentsController>
        [HttpGet]
        public ActionResult<List<TravelAgent>> Get()
        {
            return travelAgentService.Get();
        }

        // GET api/<TravelAgentsController>/5
        [HttpGet("{id}")]
        public ActionResult<TravelAgent> Get(string id)
        {
            var travelagent = travelAgentService.Get(id);
            if (travelagent == null)
            {
                return NotFound($"Travel Agent with Id = {id} not found");
            }
            return travelagent;
        }

        // POST api/<TravelAgentsController>
        [HttpPost]
        public ActionResult<TravelAgent> Post([FromBody] TravelAgent travelagent)
        {
            travelAgentService.Create(travelagent);
            return CreatedAtAction(nameof(Get), new { id = travelagent.Id }, travelagent);
        }

        // PUT api/<TravelAgentsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] TravelAgent travelagent)
        {
            var existingTravelAgent = travelAgentService.Get(id);

            if (existingTravelAgent == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }
            travelAgentService.Update(id, travelagent);
            return NoContent();
        }

        // DELETE api/<TravelAgentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var travelagent = travelAgentService.Get(id);
            if (travelagent == null)
            {
                return NotFound($"Travel Agent with Id = {id} not found");
            }
            travelAgentService.Remove(travelagent.Id);
            return Ok($"Travel Agent with Id = {id} deleted");
        }

        // POST api/<TravelAgentsController>/login
        [HttpPost("login")]
        public ActionResult<TravelAgent> Login([FromBody] TravelAgentLoginRequest request)
        {
            var travelAgent = travelAgentService.Login(request.Email, request.Password);
            if (travelAgent == null)
            {
                return NotFound("Invalid email or password");
            }
            return Ok(travelAgent);
        }


    }
}