using MongoDB.Driver;
using Train_Reservation_System.Database;
using Train_Reservation_System.Models.Reservations;
using Train_Reservation_System.Models.Trains;

namespace Train_Reservation_System.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservations;
        private readonly IMongoCollection<Train> _trains;

        public ReservationService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _reservations = database.GetCollection<Reservation>(settings.ReservationCollectionName);
            _trains = database.GetCollection<Train>(settings.TrainCollectionName);
        }
        public Reservation Create(Reservation reservation)
        {
            _reservations.InsertOne(reservation);
            return reservation;
        }

        public List<Reservation> Get()
        {
            return _reservations.Find(reservation => true).ToList();
        }

        public Reservation Get(string id)
        {
            return _reservations.Find(reservation => reservation.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _reservations.DeleteOne(reservation => reservation.Id == id);
        }

        public void Update(string id, Reservation reservation)
        {
            _reservations.ReplaceOne(reservation => reservation.Id == id, reservation);
        }

        public int GetByTrain(string trainId)
        {
            return _reservations.Find(reservation => reservation.Train == trainId).ToList().Count();
        }

        public int GetReservationsCountByNIC(string nic)
        {
            var count = _reservations.CountDocuments(reservation => reservation.NIC == nic);
            return (int)count;
        }




        // Modified GetReservedSeatsCount service
        public int GetReservedSeatsCount(string trainId, DateTime reservationDate)
        {
            reservationDate = reservationDate.Date;
            var filterBuilder = Builders<Reservation>.Filter;

            // Create a filter to match the trainId
            var trainFilter = filterBuilder.Eq(reservation => reservation.Train, trainId);

            var dateFilter = filterBuilder.Eq(reservation => reservation.ReservationDate, reservationDate);

            // Combine the train and date filters
            var combinedFilter = filterBuilder.And(trainFilter, dateFilter);

            // Find all reservations for the selected train and date
            var reservedSeatsCountArray = _reservations.Find(combinedFilter).ToList();

            int totalReservedSeats = 0;

            foreach (var seat in reservedSeatsCountArray)
            {
                totalReservedSeats += seat.NumberOfSeats;
            }

            // Find the total seats available on the train
            var train = _trains.Find(t => t.Id == trainId).FirstOrDefault();
            int totalTrainSeats = train != null ? train.TrainSeats : 0;

            // Calculate available seats by subtracting reserved seats from total seats
            int availableSeats = totalTrainSeats - totalReservedSeats;

            // Update the available seats in the train document
            var updateDefinition = Builders<Train>.Update.Set(t => t.AvailableSeats, availableSeats);
            _trains.UpdateOne(t => t.Id == trainId, updateDefinition);

            return availableSeats;
        }

        // Modified FilterTrainsAndCalculateAvailabilityAndFee service
        public List<Train> FilterTrainsAndCalculateAvailabilityAndFee(string fromStation, string toStation, DateTime reservationDate)
        {
            var filter = Builders<Train>.Filter
                .ElemMatch(train => train.Stations, station => station.StationName == fromStation)
                & Builders<Train>.Filter
                .ElemMatch(train => train.Stations, station => station.StationName == toStation)
                & Builders<Train>.Filter.Eq(train => train.IsActive, true);

            var availableTrains = _trains.Find(filter).ToList();

            foreach (var train in availableTrains)
            {
                var fromIndex = Array.FindIndex(train.Stations, station => station.StationName == fromStation);
                var toIndex = Array.FindIndex(train.Stations, station => station.StationName == toStation);

                if (fromIndex != -1 && toIndex != -1 && fromIndex < toIndex)
                {
                    reservationDate = reservationDate.Date;
                    var availableSeats = GetReservedSeatsCount(train.Id, reservationDate);
                    train.AvailableSeats = availableSeats;

                    // Calculate train fee
                    var unitPrice = train.UnitPrice;
                    var fee = (toIndex - fromIndex) * unitPrice;
                    train.Fee = fee;
                }
                else
                {
                    train.AvailableSeats = 0; // No available seats if "from" station is after "to" station
                }
            }

            // Filter trains with available seats
            var filteredTrains = availableTrains.Where(train => train.AvailableSeats > 0).ToList();

            return filteredTrains;
        }

        public List<Reservation> GetExistingReservationsByNIC(string nic)
        {

            var currentDate = DateTime.Now;
            var filter = Builders<Reservation>.Filter.Where(r => r.NIC == nic && r.ReservationDate >= currentDate);

            return _reservations.Find(filter).ToList();
        }


        public List<Reservation> GetReservationHistoryByNIC(string nic)
        {
            var currentDate = DateTime.Now;
            var filter = Builders<Reservation>.Filter.Where(r => r.NIC == nic && r.ReservationDate < currentDate);

            return _reservations.Find(filter).ToList();
        }





    }
}


