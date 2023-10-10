using MongoDB.Driver;
using Train_Reservation_System.Database;
using Train_Reservation_System.Models.BackOfficer;
using Train_Reservation_System.Models.TravelAgent;


namespace Train_Reservation_System.Services.BackOfficers
{
    public class BackOfficerService : IBackOfficerService
    {
        private readonly IMongoCollection<BackOfficer> _backofficers;

        public BackOfficerService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _backofficers = database.GetCollection<BackOfficer>(settings.BackOfficerCollectionName);
        }
        public BackOfficer Create(BackOfficer backofficer)
        {
            _backofficers.InsertOne(backofficer);
            return backofficer;
        }

        public List<BackOfficer> Get()
        {
            return _backofficers.Find(backofficer => true).ToList();
        }

        public BackOfficer Get(string id)
        {
            return _backofficers.Find(backofficer => backofficer.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _backofficers.DeleteOne(backofficer => backofficer.Id == id);
        }

        public void Update(string id, BackOfficer backofficer)
        {
            _backofficers.ReplaceOne(backofficer => backofficer.Id == id, backofficer);
        }
    }
}
