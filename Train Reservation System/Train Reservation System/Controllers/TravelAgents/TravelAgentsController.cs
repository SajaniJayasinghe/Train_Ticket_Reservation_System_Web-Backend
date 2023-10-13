using Microsoft.AspNetCore.Mvc;
using Train_Reservation_System.Helpers;
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
            try
            {
                var travelagents = travelAgentService.Get();
                return Ok(new ApiResponse(true, 200, "Success", travelagents));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        // GET api/<TravelAgentsController>/5
        [HttpGet("{id}")]
        public ActionResult<TravelAgent> Get(string id)
        {
            try
            {
                var travelagent = travelAgentService.Get(id);
                if (travelagent == null)
                {
                    return NotFound(new ApiResponse(false, 404, $"Travel Agent with Id = {id} not found", null));
                }
                return Ok(new ApiResponse(true, 200, "Success", travelagent));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        [HttpPost]
        public ActionResult<ApiResponse> CreateTravelAgent([FromBody] TravelAgent travelagent)
        {
            try
            {
                travelAgentService.Create(travelagent);
                return new ApiResponse(true, 201, "TravelAgent created successfully", travelagent);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, 500, "Error creating TravelAgent", ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] TravelAgent travelagent)
        {
            var existingTravelAgent = travelAgentService.Get(id);

            if (existingTravelAgent == null)
            {
                return NotFound(new ApiResponse(false, 401, $"TravelAgent with Id = {id} not found", null));
            }

            travelAgentService.Update(id, travelagent);

            return Ok(new ApiResponse(true, 200, "TravelAgent updated successfully", existingTravelAgent));
        }

        // DELETE api/<TravelAgentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var travelagent = travelAgentService.Get(id);
            if (travelagent == null)
            {
                return NotFound(new ApiResponse(false, 401, $"Travel Agent with Id = {id} not found", null));
            }
            travelAgentService.Remove(travelagent.Id);
            return Ok(new ApiResponse(true,200, "Success", travelagent));
        }

        // POST api/<TravelAgentsController>/login
        [HttpPost("login")]
        public ActionResult<TravelAgent> Login([FromBody] TravelAgentLoginRequest request)
        {
            var travelAgent = travelAgentService.Login(request.Email, request.Password);
            if (travelAgent == null)
            {
                return NotFound(new ApiResponse(false,401,"Invalid email or password",null));
            }
            return Ok(new ApiResponse(true,200,"Success",new {TravelAgent = travelAgent}));
        }


    }
}