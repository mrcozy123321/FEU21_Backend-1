namespace WebApi.Models
{
    public class IssueRequest
    {
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int StatusId { get; set; }
    }
}
