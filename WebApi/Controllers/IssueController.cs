using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApi.Data;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly DataContext _context;

        public IssueController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IssueRequest req)
        {
            var statusEntity = await _context.Statuses.FindAsync(req.StatusId);
            if (statusEntity != null)
            {
                var issueEntity = new IssueEntity
                {
                    StatusId = statusEntity.Id,
                    Message = req.Message,
                    Subject = req.Subject,
                    Email = req.Email,
                };

                _context.Add(issueEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(new Issue
                {
                    Id = issueEntity.Id,
                    Email = issueEntity.Email,
                    Subject = issueEntity.Subject,
                    Message = issueEntity.Message,
                    Created = DateTime.Now,
                    StatusName = statusEntity.Name
                });
            }
            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var issues = new List<Issue>();

                foreach (var issue in await _context.Issues.Include(x => x.Status).ToListAsync())
                {   
                    var status = await _context.Statuses.FindAsync(issue.StatusId);
                    issues.Add(new Issue
                    {
                        Id = issue.Id,
                        Email = issue.Email,
                        Subject = issue.Subject,
                        Message = issue.Message,
                        Created = DateTime.Now,
                        StatusName = status.Name
                    });
                }
                return new OkObjectResult(issues);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new BadRequestResult();
        }
    }
}
