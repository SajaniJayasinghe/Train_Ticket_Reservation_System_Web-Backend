using Microsoft.AspNetCore.Mvc;
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
            return backOfficerService.Get();
        }

        // GET api/<BackOfficersController>/5
        [HttpGet("{id}")]
        public ActionResult<BackOfficer> Get(string id)
        {
            var backofficer = backOfficerService.Get(id);
            if (backofficer == null)
            {
                return NotFound($"backofficer Agent with Id = {id} not found");
            }
            return backofficer;
        }

        // POST api/<BackOfficersController>
        [HttpPost]
        public ActionResult<BackOfficer> Post([FromBody] BackOfficer backofficer)
        {
            backOfficerService.Create(backofficer);
            return CreatedAtAction(nameof(Get), new { id = backofficer.Id }, backofficer);
        }

        // PUT api/<BackOfficersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] BackOfficer backofficer)
        {
            var existingBackOfficer = backOfficerService.Get(id);

            if (existingBackOfficer == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }
            backOfficerService.Update(id, backofficer);
            return NoContent();
        }

        // DELETE api/<BackOfficersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var backofficer = backOfficerService.Get(id);
            if (backofficer == null)
            {
                return NotFound($"BackOfficer Agent with Id = {id} not found");
            }
            backOfficerService.Remove(backofficer.Id);
            return Ok($"BackOfficer with Id = {id} deleted");
        }
    }
}
