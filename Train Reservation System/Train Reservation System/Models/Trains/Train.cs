using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Train_Reservation_System.Models.Trains
{
    [BsonIgnoreExtraElements]

    public class Train
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("trainName")]
        public string TrainName { get; set; } = "Train Name";

        [BsonElement("trainNumber")]
        public string TrainNumber { get; set; } = "Train Number";

        [BsonElement("trainStartTime")]
        public string TrainStartTime { get; set; } = "Train Start Time";

        [BsonElement("stations")]
        public Station[]? Stations { get; set; }

        [BsonElement("trainSeats")]
        public int TrainSeats { get; set; }

        [BsonElement("availableSeats")]
        public int AvailableSeats { get; set; }

        [BsonElement("unitPrice")]
        public int UnitPrice { get; set; }

        [BsonElement("fee")]
        public int Fee { get; set; }

        [BsonElement("active")]
        public bool IsActive { get; set; }

        public class Station
        {
            [BsonElement("stationName")]
            public string StationName { get; set; } = "Station Name";

            [BsonElement("time")]
            public string Time { get; set; } = "Time";
        }

    }
}




