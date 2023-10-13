using MongoDB.Bson;
using MongoDB.Driver;
using Train_Reservation_System.Database;
using Train_Reservation_System.Models.TravelAgent;
using Train_Reservation_System.Models.Travelers;
using BCrypt.Net;

namespace Train_Reservation_System.Services.TravelAgents
{
    public class TravelAgentService : ITravelAgentService
    {
        private readonly IMongoCollection<TravelAgent> _travelagents;

        public TravelAgentService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _travelagents = database.GetCollection<TravelAgent>(settings.TravelAgentCollectionName);
        }
        public TravelAgent Create(TravelAgent travelagent)
        
            {
                travelagent.PasswordHash = BCrypt.Net.BCrypt.HashPassword(travelagent.PasswordHash);
                travelagent.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(travelagent.ConfirmPassword);
                _travelagents.InsertOne(travelagent);
                return travelagent;
            }
        

        public List<TravelAgent> Get()
        {
            return _travelagents.Find(travelagent => true).ToList();
        }

        public TravelAgent Get(string id)
        {
            return _travelagents.Find(travelagent => travelagent.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _travelagents.DeleteOne(travelagent => travelagent.Id == id);
        }

        //public void Update(string id, TravelAgent travelagent)
        //{
        //    _travelagents.ReplaceOne(travelagent => travelagent.Id == id, travelagent);
        //}

        public void Update(string id, TravelAgent travelagent)
        {
            var existingTravelAgent = _travelagents.Find(agent => agent.Id == id).FirstOrDefault();

            if (existingTravelAgent == null)
            {
                return; // Handle not found case as needed
            }

            // Check if a new password is provided
            if (!string.IsNullOrEmpty(travelagent.PasswordHash))
            {
                // Hash the new password using BCrypt and update the PasswordHash property
                existingTravelAgent.PasswordHash = BCrypt.Net.BCrypt.HashPassword(travelagent.PasswordHash);
            }

            // Update other properties as needed
            existingTravelAgent.FullName = travelagent.FullName;
            existingTravelAgent.Email = travelagent.Email;
            existingTravelAgent.Role = travelagent.Role;
            existingTravelAgent.NIC = travelagent.NIC;
            existingTravelAgent.PhoneNumber = travelagent.PhoneNumber;

            // Replace the existing document with the updated one
            _travelagents.ReplaceOne(agent => agent.Id == id, existingTravelAgent);
        }



        public TravelAgent Login(string email, string password)
        {
            var travelAgent = _travelagents.Find(travelagent => travelagent.Email == email).FirstOrDefault();

            if (travelAgent == null)
            {
                // Travel Agent with the specified email not found
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, travelAgent.PasswordHash))
            {
                // Password does not match
                return null;
            }

            return travelAgent;
        }
    }
}




