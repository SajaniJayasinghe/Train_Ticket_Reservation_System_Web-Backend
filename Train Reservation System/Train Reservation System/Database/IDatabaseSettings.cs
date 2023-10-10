namespace Train_Reservation_System.Database
{
    public interface IDatabaseSettings
    {
        string TrainCollectionName { get; set; }
        string TravelerCollectionName { get; set; }
        string TravelAgentCollectionName { get; set; }
        string BackOfficerCollectionName { get; set; }
        string ReservationCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        
    }
}
