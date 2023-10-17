using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Diagnostics;
using Train_Reservation_System.Helpers;
using Train_Reservation_System.Models.Reservations;
using Train_Reservation_System.Models.Trains;
using Train_Reservation_System.Models.TravelAgent;
using Train_Reservation_System.Models.Travelers;
using Train_Reservation_System.Services.Reservations;
using Train_Reservation_System.Services.Travelers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Train_Reservation_System.Controllers.Reservations
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }
    
        // GET: api/<ReservationsController>
        [HttpGet]
        public ActionResult<List<Reservation>> Get()
        {
            try
            {
                var reservations = reservationService.Get();
                return Ok(new ApiResponse(true, 200, "Success", reservations));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }


        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(string id)
        {
            try
            {
                var reservation = reservationService.Get(id);
                if (reservation == null)
                {
                    return NotFound(new ApiResponse(false, 404, $"Reservation with ID = {id} not found", null));
                }
                return Ok(new ApiResponse(true, 200, "Success", reservation));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }


        [HttpPost]
        public ActionResult<ApiResponse> Post([FromBody] Reservation reservation)
        {
            try
            {
                reservation.ReservationDate = reservation.ReservationDate.Date;
                reservation.BookingDate = reservation.BookingDate.Date;

                // Validate the reservation date (within 30 days from booking date)
                if ((reservation.ReservationDate - reservation.BookingDate).TotalDays > 30)
                {
                    return BadRequest("Reservation date must be within 30 days from the booking date.");
                }

                // Query the database to get the existing reservations count by NIC
                int existingReservationsCount = reservationService.GetReservationsCountByNIC(reservation.NIC);

                // Log the existingReservationsCount for debugging purposes
                Console.WriteLine($"Existing Reservations Count for NIC '{reservation.NIC}': {existingReservationsCount}");

                if (existingReservationsCount >= 4)
                {
                    return BadRequest("Maximum 4 reservations allowed per NIC.");
                }
                
                // If a train is selected, create the reservation
                reservationService.Create(reservation);
                return new ApiResponse(true, 201, "Reservation created successfully", reservation);
            }
            catch (Exception ex)
            {
                
                return new ApiResponse(false, 500, "Error creating Reservation", ex.Message);
            }
        }

        [HttpGet("filterTrains")]
        public ActionResult<List<Train>> FilterTrainsAndCalculateAvailabilityAndFee(
     [FromQuery] string fromStation,
     [FromQuery] string toStation,
     [FromQuery] DateTime reservationDate)


        {
            try
            {
                reservationDate = reservationDate.Date;
                var filteredTrains = reservationService.FilterTrainsAndCalculateAvailabilityAndFee(fromStation, toStation, reservationDate);
                return Ok(new ApiResponse(true, 200, "Success", filteredTrains));
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate error response
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }


        // PUT api/<ReservationsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Reservation reservation)
        {
            var existingReservation = reservationService.Get(id);

            if (existingReservation == null)
            {
                return NotFound(new ApiResponse(false, 401, $"Reservation with ID = {id} not found", null));
            }

            // Ensure that reservations can be updated at least 5 days before the reservation date
            var daysUntilReservation = (existingReservation.ReservationDate - DateTime.Now).Days;
            if (daysUntilReservation < 5)
            {
                return BadRequest("Reservations can only be updated at least 5 days before the reservation date.");
            }

            reservationService.Update(id, reservation);
            return Ok(new ApiResponse(true, 200, "Reservation updated successfully", existingReservation));
        }


        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var reservation = reservationService.Get(id);
            if (reservation == null)
            {
                return NotFound(new ApiResponse(false, 401, $"Reservation with ID = {id} not found", null));
            }

            // Ensure that reservations can be canceled at least 5 days before the reservation date
            var daysUntilReservation = (reservation.ReservationDate - DateTime.Now).Days;
            if (daysUntilReservation < 5)
            {
                return BadRequest("Reservations can only be canceled at least 5 days before the reservation date.");
            }

            reservationService.Remove(reservation.Id);
            return Ok(new ApiResponse(true, 200, "Success", reservation));
        }

        //get existing reservations
        [HttpGet("getExistingByNIC/{nic}")]
        public ActionResult<List<Reservation>> GetExistingReservationsByNIC(string nic)
        {
            try
            {
                var reservations = reservationService.GetExistingReservationsByNIC(nic);
                return Ok(new ApiResponse(true, 200, "Success", reservations));
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate error response
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }

        //get history reservations
        [HttpGet("getHistoryByNIC/{nic}")]
        public ActionResult<List<Reservation>> GetReservationHistoryByNIC(string nic)
        {
            try
            {
                var reservationHistory = reservationService.GetReservationHistoryByNIC(nic);
                return Ok(new ApiResponse(true, 200, "Success", reservationHistory));
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate error response
                return StatusCode(500, new ApiResponse(false, 500, ex.Message, null));
            }
        }



    }
}


