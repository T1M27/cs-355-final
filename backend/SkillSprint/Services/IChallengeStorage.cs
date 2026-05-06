using SkillSprint.Models;

namespace SkillSprint.Services
{
    public interface IChallengeStorage
    {
        List<Challenge> GetAllChallenges();
        Challenge GetOneChallenge(int id);
        Task<Challenge> AddChallenge(Challenge input);
        Task<Challenge> UpdateChallenge(Challenge input);
        Task<bool> DeleteChallenge(int id);
    }
}
