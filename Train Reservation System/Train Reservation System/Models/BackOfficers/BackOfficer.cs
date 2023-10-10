using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Train_Reservation_System.Models.BackOfficer
{
    public class BackOfficer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("full name")]
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
