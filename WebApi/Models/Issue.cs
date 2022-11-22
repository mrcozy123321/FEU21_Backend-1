namespace WebApi.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;
        public string StatusName { get; set; }
    }
}
