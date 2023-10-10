using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Train_Reservation_System.Models.Reservations
{
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("nic")]
        public string NIC { get; set; } = "NIC";

        [BsonElement("bookingDate")]
        public DateTime BookingDate { get; set; }

        [BsonElement("reservationDate")]
        public DateTime ReservationDate { get; set; }

        [BsonElement("fromStation")]
        public string FromStation { get; set; } = "From Station";

        [BsonElement("toStation")]
        public string ToStation { get; set; } = "To Station";

        [BsonElement("trainName")]
        public string TrainName { get; set; } = "Train Name";

        [BsonElement("train")]
        public string Train { get; set; } = "Train";

        [BsonElement("numberOfSeats")]
        public int NumberOfSeats { get; set; }

    }
}
