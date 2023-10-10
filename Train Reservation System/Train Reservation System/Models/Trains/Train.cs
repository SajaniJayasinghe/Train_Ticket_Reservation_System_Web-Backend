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

        // [BsonElement("trainRoute")]
        // public string TrainRoute { get; set; } = "Train Route";

        // [BsonElement("trainFare")]
        // public string TrainFare { get; set; } = "Train Fare";

        //[BsonElement("trainStatus")]
        //public byte TrainStatus { get; set; } = 1;

        //[BsonElement("trainStatus")]
        //public bool TrainStatus { get; set; }

        //[BsonElement("trainDateTime")]
        //public DateTime TrainDateTime { get; set; }

        //[BsonElement("trainPlatform")]
        //public string TrainPlatform { get; set; } = "Train Platform";

        // [BsonElement("trainArrival")]
        //public string TrainArrival { get; set; } = "Train Arrival";

        //[BsonElement("trainDeparture")]
        //public string TrainDeparture { get; set; } = "Train Departure";

        //[BsonElement("trainDistance")]
        // public string TrainDistance { get; set; } = "Train Distance";

        // [BsonElement("trainDuration")]
        //public string TrainDuration { get; set; } = "Train Duration";

        //[BsonElement("trainStationName")]
        //public string TrainStationName { get; set; } = "Train Station Name";
    }
}



