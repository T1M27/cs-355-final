using SkillSprint.Models;

namespace SkillSprint.Services
{
    public class ChallengeService
    {
        private static List<Challenge> allChallenges;

        public ChallengeService()
        {
            // instantiate my allChallenges
            if(allChallenges == null)
            {
                allChallenges = new List<Challenge>();

                // make a challenge
                Challenge output = new Challenge();
                output.Id = 1;
                output.Title = "Square Root Function";
                output.Difficulty = "Easy";
                output.PostedBy = "Some Guy";

                allChallenges.Add(output);
            }
        }

        // retrieve all challenges
        public List<Challenge> GetAllChallenges()
        {
            return allChallenges;
        }
        public Challenge GetOneChallenge(int id)
        {
            // loop through challenges and find the correct one
            foreach(Challenge challenge in allChallenges)
            {
                if(challenge.Id == id)
                {
                    return challenge;
                }
                
            }
            // if no challenge was found, return null
            return null;
        }

        // add new challenge method
        public Challenge AddChallenge(Challenge input)
        {
            input.Id = allChallenges.Count + 1;
            allChallenges.Add(input);
            return input;
        }

        // update a card
        public Challenge UpdateChallenge(Challenge input)
        {
            for(int i = 0; i < allChallenges.Count; i++)
            {
                if (allChallenges[i].Title == input.Title)
                {
                    allChallenges[i].Difficulty = input.Difficulty;
                }
            }
            return input;
        }

        // delete
        public bool DeleteChallenge(int id)
        {
            Challenge challengeToDelete = GetOneChallenge(id);
            if(challengeToDelete != null)
            {
                allChallenges.Remove(challengeToDelete);
                return true;
            }
            return false;
        }


    }
}
