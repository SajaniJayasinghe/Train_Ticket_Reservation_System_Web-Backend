using MongoDB.Driver;
using Train_Reservation_System.Database;
using Train_Reservation_System.Models.Trains;
using Train_Reservation_System.Services.Reservations;

namespace Train_Reservation_System.Services.Trains

{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trains;
        private readonly IReservationService reservationService;



        public TrainService(IDatabaseSettings settings, IMongoClient mongoClient, IReservationService reservationService)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>(settings.TrainCollectionName);

            this.reservationService = reservationService;
        }
        public Train Create(Train train)
        {
            _trains.InsertOne(train);
            return train;
        }

        public List<Train> Get()
        {
            return _trains.Find(train => true).ToList();
        }

        public Train Get(string id)
        {
            return _trains.Find(train => train.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            int reservationcount = reservationService.GetByTrain(id);
            // check if there are any reservations for this train
            if (reservationcount > 0)
            {
                throw new Exception("cannot delete a train with existing reservations.");
            }

            _trains.DeleteOne(train => train.Id == id);
        }

       

        public void Update(string id, Train train)
        {
            _trains.ReplaceOne(train => train.Id == id, train);
        }
    }
}

