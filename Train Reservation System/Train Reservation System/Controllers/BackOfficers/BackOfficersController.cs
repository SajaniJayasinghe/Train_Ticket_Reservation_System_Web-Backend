using Microsoft.AspNetCore.Mvc;
using Train_Reservation_System.Helpers;
using Train_Reservation_System.Models.BackOfficer;
using Train_Reservation_System.Services.BackOfficers;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Train_Reservation_System.Controllers.BackOfficers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackOfficersController : ControllerBase
    {
        private readonly IBackOfficerService backOfficerService;

        public BackOfficersController(IBackOfficerService backOfficerService)
        {
            this.backOfficerService = backOfficerService;
        }
        // GET: api/<BackOfficersController>
        [HttpGet]
        public ActionResult<List<BackOfficer>> Get()
        {
            try
            {
                var travelagents = backOfficerService.Get();
                return Ok(new ApiResponse(true, 200, "Success", travelagents));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        // GET api/<BackOfficersController>/5
        [HttpGet("{id}")]
        public ActionResult<BackOfficer> Get(string id)
        {
            try
            {
                var travelagent = backOfficerService.Get(id);
                if (travelagent == null)
                {
                    return NotFound(new ApiResponse(false, 404, $"Back Officer with Id = {id} not found", null));
                }
                return Ok(new ApiResponse(true, 200, "Success", travelagent));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        // POST api/<BackOfficersController>
        [HttpPost]
        public ActionResult<ApiResponse> Post([FromBody] BackOfficer backofficer)
        {
            try
            {
                backOfficerService.Create(backofficer);
                return new ApiResponse(true, 201, "TravelAgent created successfully", backofficer);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, 500, "Error creating TravelAgent", ex.Message);
            }
        }

        // PUT api/<BackOfficersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] BackOfficer backofficer)
        {
            var existingBackOfficer = backOfficerService.Get(id);

            if (existingBackOfficer == null)
            {
                return NotFound(new ApiResponse(false, 401, $"BackOfficer  with Id = {id} not found", null));
            }
            backOfficerService.Update(id, backofficer);
            return Ok(new ApiResponse(true, 200, "BackOfficer updated successfully", existingBackOfficer));
        }

        // DELETE api/<BackOfficersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var backofficer = backOfficerService.Get(id);
            if (backofficer == null)
            {
                return NotFound(new ApiResponse(false, 401, $"BackOfficer  with Id = {id} not found", null));
            }
            backOfficerService.Remove(backofficer.Id);
            return Ok(new ApiResponse(true, 200, "Success", backofficer));
        }
    }
}
