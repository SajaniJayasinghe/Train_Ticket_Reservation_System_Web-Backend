using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Train_Reservation_System.Models.Travelers
{
    public class Traveler
    {
        [BsonId]
        [BsonElement("nic")]
        public string NIC { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; } = String.Empty;

        [BsonElement("lastName")]
        public string LastName { get; set; } = String.Empty;

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string PasswordHash { get; set; } // Store password hash

        [BsonElement("confirmPassword")]
        public string ConfirmPassword { get; set; } // Only used during registration

        [BsonElement("phone")]
        public string Phone { get; set; } = String.Empty;

        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }
}
