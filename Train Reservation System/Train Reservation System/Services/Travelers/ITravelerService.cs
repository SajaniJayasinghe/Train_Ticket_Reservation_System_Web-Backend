using Train_Reservation_System.Models.Travelers;

namespace Train_Reservation_System.Services.Travelers
{
    public interface ITravelerService 
    {
        List<Traveler> Get();
        Traveler Get(string id);
        Traveler Create(Traveler traveler);
        void Update(string id, Traveler traveler);
        void Remove(string id);
        Traveler Login(string nic, string password);
        void Activate(string nic);
        void Deactivate(string nic);
    }
}
