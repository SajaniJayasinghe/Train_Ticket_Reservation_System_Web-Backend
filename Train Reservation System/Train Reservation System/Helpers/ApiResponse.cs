namespace Train_Reservation_System.Helpers
{
    public class ApiResponse
    {
        public bool IsSuccessful { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }


        public ApiResponse(bool isSuccessful, int statusCode, string message, object data)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            Message = message;
            Data = data;

        }
    }
}
