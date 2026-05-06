using SkillSprint.Models;

namespace SkillSprint.Services
{
    public interface IChallengeSource
    {
        Task<List<Challenge>> GetChallenges();
    }
}
