namespace Train_Reservation_System.Database
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string TrainCollectionName { get; set; } = string.Empty;
        public string TravelerCollectionName { get; set; } = string.Empty;
        public string TravelAgentCollectionName { get; set; } = string.Empty;
        public string BackOfficerCollectionName { get; set; } = string.Empty;
        public string ReservationCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
