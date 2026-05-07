using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillSprint.Data;
using SkillSprint.Services;

namespace SkillSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChallengesController : ControllerBase
    {
        private ChallengeService challengeService = new ChallengeService();
        
        private IChallengeStorage challengeStorage;

        private SkillSprintContext _context;
        public ChallengesController(SkillSprintContext _context, IChallengeStorage challengeStorage)
        {
            this._context = _context;
            this.challengeStorage = challengeStorage;
            
        }

        [HttpGet]
        public IActionResult GrabAllChallenges(string? title, string? difficulty, string? postedBy)
        {
            List<Models.Challenge> output = challengeStorage.GetAllChallenges();
            
            if (!string.IsNullOrEmpty(title) )
            {
                output = output.Where(c => c.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            if (title != null && title.StartsWith("a",StringComparison.InvariantCultureIgnoreCase))
            {
                output = output.OrderBy(c => c.Title).ToList();
            } else if (title != null)
            {
                output = output.OrderByDescending(c => c.Title).ToList();
            }
            if (difficulty != null && difficulty.StartsWith("a", StringComparison.InvariantCultureIgnoreCase))
            {
                output = output.OrderBy(c => c.Difficulty).ToList();
            }
            else if (title != null)
            {
                output = output.OrderByDescending(c => c.Difficulty).ToList();
            }
            if (postedBy != null && postedBy.StartsWith("a", StringComparison.InvariantCultureIgnoreCase))
            {
                output = output.OrderBy(c => c.PostedBy).ToList();
            }
            else if (postedBy != null)
            {
                output = output.OrderByDescending(c => c.PostedBy).ToList();
            }

            // send it back to the requestor
            return Ok(output);
        }

        [HttpGet("{id}")]
        public IActionResult GrabOneChallenge(int id)
        {
            return Ok(challengeStorage.GetOneChallenge(id));
        }

        // add a new challenge from user
        [HttpPost]
        public async Task<IActionResult> AddUsersChallenge(Models.Challenge usersChallenge)
        {
            Models.Challenge addedChallenge = await challengeStorage.AddChallenge(usersChallenge);
            return CreatedAtAction(nameof(GrabOneChallenge), new { id = addedChallenge.Title }, addedChallenge);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChallenge(string id, Models.Challenge usersChallenge)
        {
            Models.Challenge updatedChallenge = await challengeStorage.UpdateChallenge(usersChallenge);
            return Ok(updatedChallenge);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallenge(int id)
        {
            if (await challengeStorage.DeleteChallenge(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
