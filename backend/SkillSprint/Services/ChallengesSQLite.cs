using SkillSprint.Data;
using SkillSprint.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SkillSprint.Services
{
    public class ChallengesSQLite : IChallengeStorage
    {
        private SkillSprintContext _context;
        public ChallengesSQLite(SkillSprintContext context)
        {
            _context = context;
        }
        public async Task<Challenge> AddChallenge(Challenge input)
        {
            return input;
        }

        public Challenge GetOneChallenge(int id)
        {
            return _context.Challenges.FirstOrDefault(c => c.Id == id);
        }
        public async Task<bool> DeleteChallenge(int id)
        {
            try
            {
                _context.Challenges.Remove(GetOneChallenge(id));
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Challenge> GetAllChallenges()
        {
            return _context.Challenges.ToList();
        }
        public async Task<Challenge> UpdateChallenge(Challenge input)
        {
            Challenge exisitingChallenge = GetOneChallenge(input.Id);
            if (exisitingChallenge != null)
            {
                exisitingChallenge.Title = input.Title;
                exisitingChallenge.Difficulty = input.Difficulty;
                exisitingChallenge.Description = input.Description;
                exisitingChallenge.PostedBy = input.PostedBy;
                exisitingChallenge.Tags = input.Tags;
                await _context.SaveChangesAsync();
            }
            return exisitingChallenge;
        }
    }
}
