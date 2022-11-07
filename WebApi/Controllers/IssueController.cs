using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var issueEntity = new IssueEntity
            {
                StatusId = req.StatusId,
                Message = req.Message,
                Subject = req.Subject,
                Email = req.Email,
            };

            _context.Add(issueEntity);
            await _context.SaveChangesAsync();

            return new OkObjectResult(issueEntity);
        }
    }
}
