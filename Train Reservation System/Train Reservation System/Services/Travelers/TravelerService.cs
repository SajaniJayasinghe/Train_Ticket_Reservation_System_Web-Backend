using MongoDB.Driver;
using Train_Reservation_System.Database;
using Train_Reservation_System.Models.Travelers;

namespace Train_Reservation_System.Services.Travelers
{
    public class TravelerService : ITravelerService
    {
        private readonly IMongoCollection<Traveler> _traveler;

        public TravelerService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _traveler = database.GetCollection<Traveler>(settings.TravelerCollectionName);
        }
      
        public Traveler Create(Traveler traveler)
        {
            // Hash the password before storing it
            traveler.PasswordHash = BCrypt.Net.BCrypt.HashPassword(traveler.PasswordHash);
            traveler.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(traveler.ConfirmPassword);
            _traveler.InsertOne(traveler);
            return traveler;
        }

        public List<Traveler> Get()
        {
            return _traveler.Find(traveler => true).ToList();
        }

        public Traveler Get(string nic)
        {
            return _traveler.Find(traveler => traveler.NIC == nic).FirstOrDefault();
        }

        public void Remove(string nic)
        {
            _traveler.DeleteOne(traveler => traveler.NIC == nic);
        }

        public void Update(string nic, Traveler traveler)
        {
            _traveler.ReplaceOne(traveler => traveler.NIC == nic, traveler);
        }

        //public Traveler Login(string nic, string password)
        //{
        //    var traveler = _traveler.Find(traveler => traveler.NIC == nic).FirstOrDefault();

        //    if (traveler == null)
        //    {
        //        return null;
        //    }
        //    if (!traveler.IsActive)
        //    {
        //        return null;
        //    }

        //    if (traveler.PasswordHash != password)
        //    {
        //        return null;
        //    }

        //    return traveler;
        //}

        public Traveler Login(string nic, string password)
        {
            var traveler = _traveler.Find(t => t.NIC == nic).FirstOrDefault();

            if (traveler == null)
            {
                return null; 
            }
            if (!traveler.IsActive)
            {
                return null; 
            }

            if (!BCrypt.Net.BCrypt.Verify(password, traveler.PasswordHash))
            {
                return null; 
            }

            return traveler; 
        }
        public void Activate(string nic)
        {
            var filter = Builders<Traveler>.Filter.Eq(traveler => traveler.NIC, nic);
            var update = Builders<Traveler>.Update.Set(traveler => traveler.IsActive, true);

            _traveler.UpdateOne(filter, update);
        }

        public void Deactivate(string nic)
        {
            var filter = Builders<Traveler>.Filter.Eq(traveler => traveler.NIC, nic);
            var update = Builders<Traveler>.Update.Set(traveler => traveler.IsActive, false);

            _traveler.UpdateOne(filter, update);
        }
    }
}
