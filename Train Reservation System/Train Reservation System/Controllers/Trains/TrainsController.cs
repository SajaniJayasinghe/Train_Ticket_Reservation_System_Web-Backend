using Microsoft.AspNetCore.Mvc;
using Train_Reservation_System.Models.Trains;
using Train_Reservation_System.Services.Trains;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Train_Reservation_System.Controllers.Trains
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly ITrainService trainService;

        public TrainsController(ITrainService trainService)
        {
            this.trainService = trainService;
        }
        // GET: api/<TrainsController>
        [HttpGet]
        public ActionResult<List<Train>> Get()
        {
            return trainService.Get();
        }


        // GET api/<TrainsController>/5
        [HttpGet("{id}")]
        public ActionResult<Train> Get(string id)
        {
            var train = trainService.Get(id);
            if (train == null)
            {
                return NotFound($"Train with Id = {id} not found");
            }
            return train;
        }

        // POST api/<TrainsController>
        [HttpPost]
        public ActionResult<Train> Post([FromBody] Train train)
        {
            trainService.Create(train);
            return CreatedAtAction(nameof(Get), new { id = train.Id }, train);
        }

        // PUT api/<TrainsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Train train)

        {
            var existingTrain = trainService.Get(id);

            if (existingTrain == null)
            {
                return NotFound($"Train with Id = {id} not found");
            }
            trainService.Update(id, train);
            return NoContent();
        }



        // DELETE api/<TrainsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        //{
        //    var train = trainService.Get(id);
        //    if (train == null)
        //    {
        //        return NotFound($"Train with Id = {id} not found");
        //    }

        //    try
        //    {
        //        trainService.Remove(train.Id);
        //        return Ok($"Train with Id = {id} deleted");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}
        {
            var train = trainService.Get(id);
            if (train == null)
            {
                return NotFound($"Train with Id = {id} not found");
            }

            try
            {
                trainService.Remove(train.Id);
                return Ok($"Train with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}