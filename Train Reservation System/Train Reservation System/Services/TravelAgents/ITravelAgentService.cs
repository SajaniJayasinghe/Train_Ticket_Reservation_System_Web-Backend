using Train_Reservation_System.Models.TravelAgent;

namespace Train_Reservation_System.Services.TravelAgents
{
    public interface ITravelAgentService
    {
        List<TravelAgent> Get();
        TravelAgent Get(string id);
        TravelAgent Create(TravelAgent travelagent);
        void Update(string id, TravelAgent travelagent);
        void Remove(string id);
        TravelAgent Login(string email, string password);
    }
}
