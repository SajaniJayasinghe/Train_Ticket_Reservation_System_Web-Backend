using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace Train_Reservation_System.Models.TravelAgent
{
    public class TravelAgent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("fullname")]
        public string FullName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("nic")]
        public string NIC { get; set; }

        [BsonElement("password")]
        public string PasswordHash { get; set; } // Store password hash

        [BsonElement("confirmPassword")]
        public string ConfirmPassword { get; set; }

        [BsonElement("phonenumber")]
        public string PhoneNumber { get; set; }

    }
}
