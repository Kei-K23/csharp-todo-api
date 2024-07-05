namespace TodoAPI.Models
{
    public class ErrorResponse
    {
        public string Title { get; set; }
        public string ErrorMsg { get; set; }
        public int StatusCode { get; set; }
    }
}