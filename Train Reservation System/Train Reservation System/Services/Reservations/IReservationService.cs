using Train_Reservation_System.Models.Reservations;
using Train_Reservation_System.Models.Trains;

namespace Train_Reservation_System.Services.Reservations
{
    public interface IReservationService
    {
        List<Reservation> Get();
        Reservation Get(string id);
        Reservation Create(Reservation reservation);
        void Update(string id, Reservation reservation);
        void Remove(string id);
        int GetReservationsCountByNIC(string nic);
        int GetByTrain(string trainId);
        List<Train> FilterTrainsAndCalculateAvailabilityAndFee(string fromStation, string toStation, DateTime reservationDate);
        int GetReservedSeatsCount(string trainId, DateTime reservationDate);
    }
}
