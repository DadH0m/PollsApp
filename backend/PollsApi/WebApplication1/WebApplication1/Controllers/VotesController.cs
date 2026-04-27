using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollsApi.Data;
using PollsApi.Models;

namespace PollsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VotesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Vote([FromBody] VoteRequest request)
        {
            var sessionId = Request.Headers["X-Session-Id"].ToString();
            if (string.IsNullOrEmpty(sessionId))
                sessionId = Guid.NewGuid().ToString();

            var existingVote = await _context.Votes
                .FirstOrDefaultAsync(v => v.PollId == request.PollId && v.SessionId == sessionId);

            if (existingVote != null)
                return BadRequest(new { message = "Вы уже голосовали" });

            var vote = new Vote
            {
                PollId = request.PollId,
                OptionId = request.OptionId,
                SessionId = sessionId
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            return Ok(new { sessionId, message = "Голос учтён" });
        }
    }

    public class VoteRequest
    {
        public int PollId { get; set; }
        public int OptionId { get; set; }
    }
}