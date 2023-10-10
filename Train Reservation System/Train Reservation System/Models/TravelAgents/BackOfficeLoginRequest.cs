namespace Train_Reservation_System.Models.TravelAgents
{
    public class BackOfficeLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
