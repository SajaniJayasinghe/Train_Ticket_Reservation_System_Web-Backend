using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Train_Reservation_System.Models.Reservations;
using Train_Reservation_System.Models.Trains;
using Train_Reservation_System.Services.Reservations;

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
            return reservationService.Get();
        }

        // GET api/<ReservationsController>/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(string id)
        {
            var reservation = reservationService.Get(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with Id = {id} not found");
            }
            return reservation;
        }


        [HttpPost]
        public ActionResult<Reservation> Post([FromBody] Reservation reservation)
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
                Console.WriteLine(reservation.FromStation);
                // Perform train filtering based on fromStation, toStation, reservationDate
                string fromStation = reservation.FromStation;
                string toStation = reservation.ToStation;
                DateTime reservationDate = reservation.ReservationDate;

                // Log before calling FilterTrainsAndCalculateAvailabilityAndFee
                Console.WriteLine("Before calling FilterTrainsAndCalculateAvailabilityAndFee");

                // Call FilterTrainsAndCalculateAvailabilityAndFee
                List<Train> filteredTrains = reservationService.FilterTrainsAndCalculateAvailabilityAndFee(
                    fromStation, toStation, reservationDate);

                // Log after calling FilterTrainsAndCalculateAvailabilityAndFee
                Console.WriteLine("After calling FilterTrainsAndCalculateAvailabilityAndFee");


                // Log the filtered trains for debugging purposes
                Console.WriteLine("Filtered Trains:");
                foreach (var train in filteredTrains)
                {
                    Console.WriteLine($"Train ID: {train.Id}, Train Name: {train.TrainName}, Available Seats: {train.TrainSeats}, Fee: {train.Fee}");
                }

                // Check if a train is selected by the user (you need to implement this logic)
                if (string.IsNullOrWhiteSpace(reservation.Train))
                {
                    return BadRequest("Please select a train.");
                }

                // If a train is selected, create the reservation
                reservationService.Create(reservation);
                return CreatedAtAction(nameof(Get), new { id = reservation.Id },  filteredTrains);
            }
            catch (Exception ex)
            {
                // Log and handle any exceptions here
                Console.WriteLine($"Error creating reservation: {ex.StackTrace}");
                return StatusCode(500, "An error occurred while creating the reservation.");
            }
        }




        // PUT api/<ReservationsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Reservation reservation)
        {
            var existingReservation = reservationService.Get(id);

            if (existingReservation == null)
            {
                return NotFound($"Reservation with Id = {id} not found");
            }

            // Ensure that reservations can be updated at least 5 days before the reservation date
            var daysUntilReservation = (existingReservation.ReservationDate - DateTime.Now).Days;
            if (daysUntilReservation < 5)
            {
                return BadRequest("Reservations can only be updated at least 5 days before the reservation date.");
            }

            reservationService.Update(id, reservation);
            return NoContent();
        }

        // DELETE api/<ReservationsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var reservation = reservationService.Get(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with Id = {id} not found");
            }

            // Ensure that reservations can be canceled at least 5 days before the reservation date
            var daysUntilReservation = (reservation.ReservationDate - DateTime.Now).Days;
            if (daysUntilReservation < 5)
            {
                return BadRequest("Reservations can only be canceled at least 5 days before the reservation date.");
            }

            reservationService.Remove(reservation.Id);
            return Ok($"Reservation with Id = {id} deleted");
        }
    }
}
