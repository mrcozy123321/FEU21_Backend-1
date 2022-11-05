﻿namespace WebApi.Models.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;
        public int IssueId { get; set; }

        public IssueEntity Issue { get; set; }
    }
}
