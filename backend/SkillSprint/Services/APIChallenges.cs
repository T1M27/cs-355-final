using SkillSprint.Models;

namespace SkillSprint.Services
{
    public class APIChallenges : IChallengeSource
    {
        //private HttpClient httpClient;
        private IChallengeStorage challengeStorage;
        public APIChallenges(IChallengeStorage challengeStorage)
        {
            this.challengeStorage = challengeStorage;
        }
        public async Task<List<Challenge>> GetChallenges()
        {
            /*HttpResponseMessage response;
            try
            {
                
            }
            catch (Exception e)
            {
                throw new HttpRequestException("Error");
            }
            // check if server returns something
            response.EnsureSuccessStatusCode();
            // get info from server and store in variable
            ChallengeDTO result = await response.Content.ReadFromJsonAsync<ChallengeDTO>();
            List<Challenge> allChallenges = result?.docs;

            var random = new Random();
            var randomChallenges = allChallenges.OrderBy(x => random.Next()).Take(10).ToList();
            return randomChallenges;*/
            return null;
        }
    }
}
