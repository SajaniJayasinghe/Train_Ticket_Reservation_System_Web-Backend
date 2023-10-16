using Microsoft.AspNetCore.Mvc;
using Train_Reservation_System.Helpers;
using Train_Reservation_System.Models.Trains;
using Train_Reservation_System.Models.Travelers;
using Train_Reservation_System.Services.Trains;
using Train_Reservation_System.Services.Travelers;

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
            try
            {
                var trains = trainService.Get();
                return Ok(new ApiResponse(true, 200, "Success", trains));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        // GET api/<TrainsController>/5
        [HttpGet("{id}")]
        public ActionResult<Train> Get(string id)
        {
            try
            {
                var train = trainService.Get(id);
                if (train == null)
                {
                    return NotFound(new ApiResponse(false, 404, $"Train with ID = {id} not found", null));
                }
                return Ok(new ApiResponse(true, 200, "Success", train));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        // POST api/<TrainsController>
        [HttpPost]
        public ActionResult<ApiResponse> CreateTrain([FromBody] Train train)
        {
            try
            {
                trainService.Create(train);
                return new ApiResponse(true, 201, "Train created successfully", train);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, 500, "Error creating Train", ex.Message);
            }
        }

        // PUT api/<TrainsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Train train)
        {
            var existingTrain = trainService.Get(id);

            if (existingTrain == null)
            {
                return NotFound(new ApiResponse(false, 401, $"Train with ID = {id} not found", null));
            }

            trainService.Update(id, train);

            return Ok(new ApiResponse(true, 200, "Train updated successfully", existingTrain));
        }


            // DELETE api/<TrainsController>/5
            [HttpDelete("{id}")]
            public ActionResult Delete(string id)
            {
                var train = trainService.Get(id);
                if (train == null)
                {
                    return NotFound(new ApiResponse(false, 401, $"Train with ID = {id} not found", null));
                }
                trainService.Remove(train.Id);
                return Ok(new ApiResponse(true, 200, "Success", train));
            }

        }
    }
