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

        //private string _passwordHash;

        //[BsonElement("password")]
        //public string PasswordHash
        //{
        //    get => _passwordHash;
        //    set
        //    {
        //        // Hash the password using BCrypt before setting it
        //        _passwordHash = BCrypt.Net.BCrypt.HashPassword(value);
        //    }
        //}


        //private string _confirmPasswordHash;

        //[BsonElement("confirmpassword")]
        //public string ConfirmPassword
        //{
        //    get => _confirmPasswordHash;
        //    set
        //    {
        //        // Hash the confirm password using BCrypt before setting it
        //        _confirmPasswordHash = BCrypt.Net.BCrypt.HashPassword(value);
        //    }
        //}

        [BsonElement("password")]
        public string PasswordHash { get; set; } // Store password hash

        [BsonElement("confirmPassword")]
        public string ConfirmPassword { get; set; }

        [BsonElement("phonenumber")]
        public string PhoneNumber { get; set; }

    }
}
