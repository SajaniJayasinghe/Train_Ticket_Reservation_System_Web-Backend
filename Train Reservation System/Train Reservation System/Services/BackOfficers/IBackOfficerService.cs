using Train_Reservation_System.Models.BackOfficer;


namespace Train_Reservation_System.Services.BackOfficers
{
    public interface IBackOfficerService
    {
        List<BackOfficer> Get();
        BackOfficer Get(string id);
        BackOfficer Create(BackOfficer backofficer);
        void Update(string id, BackOfficer backofficer);
        void Remove(string id);
    }
}
