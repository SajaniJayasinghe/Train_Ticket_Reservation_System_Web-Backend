using Train_Reservation_System.Models.Trains;

namespace Train_Reservation_System.Services.Trains
{
    public interface ITrainService
    {
        List<Train> Get();
        Train Get(string id);
        Train Create(Train train);
        void Update(string id, Train train);
        void Remove(string id);

    }
}
