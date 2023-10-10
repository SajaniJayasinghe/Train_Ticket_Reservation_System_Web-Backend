using MongoDB.Driver;
using Train_Reservation_System.Database;
using Train_Reservation_System.Models.Trains;

namespace Train_Reservation_System.Services.Trains

{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trains;
        
        public TrainService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _trains = database.GetCollection<Train>(settings.TrainCollectionName);
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

        //public void Remove(string id)
        //{
        //    int reservationCount = reservationService.GetByTrain(id);
        //    // Check if there are any reservations for this train
        //    if (reservationCount > 0)
        //    {
        //        throw new Exception("Cannot delete a train with existing reservations.");
        //    }

        //    _trains.DeleteOne(train => train.Id == id);
        //}

        public void Remove(string id)
        {
            _trains.DeleteOne(train => train.Id == id);
        }

        public void Update(string id, Train train)
        {
            _trains.ReplaceOne(train => train.Id == id, train);
        }
    }
}

