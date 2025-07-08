using T2LifestyleChecker.Utils;

namespace T2LifestyleChecker.Service
{
    public class GenerateRiskService : IGenerateRiskService
    {
        public Task<int> GetRisk(bool smoker, bool drinker, bool exercise, DateOnly dateOfBirth)
        {
            var result = 0;
            var age = CheckerUtils.CalculateAge(dateOfBirth);

            var currentAgeBracket = Constants.OverSixtyFive;

            if (age >= 16 && age <= 21)
            {
                currentAgeBracket = Constants.SixteenToTwentyOne;
            }
            if (age >=22 && age <= 40)
            {
                currentAgeBracket = Constants.TwentyTwoToForty;
            }
            if (age >= 41 && age <= 65)
            {
                currentAgeBracket = Constants.FortyOneToSixtyFive;
            }

            // Using a dictionary of dictionaries for maximum flexibility.
            // Potentially this could be moved into a data store - a document or relational database 
            // or API call to allow for scores to be changed dynamically without having to re-release the application
            var riskQuestionScores = new Dictionary<string, Dictionary<string, int>>
            {
                { Constants.Question1, new Dictionary<string, int> {
                    { Constants.SixteenToTwentyOne, 1 }, { Constants.TwentyTwoToForty, 2 }, { Constants.FortyOneToSixtyFive, 3 }, { Constants.OverSixtyFive, 3 }
                }},
                { Constants.Question2, new Dictionary<string, int> {
                    { Constants.SixteenToTwentyOne, 2 }, { Constants.TwentyTwoToForty, 2 }, { Constants.FortyOneToSixtyFive, 2 }, { Constants.OverSixtyFive, 3 }
                }},
                { Constants.Question3, new Dictionary<string, int> {
                    { Constants.SixteenToTwentyOne, 1 }, { Constants.TwentyTwoToForty, 3 }, { Constants.FortyOneToSixtyFive, 2 }, { Constants.OverSixtyFive, 1 }
                }}
            };

            foreach (var questionEntry in riskQuestionScores)
            {
                string question = questionEntry.Key;
                var ageData = questionEntry.Value;

                foreach (var ageEntry in ageData)
                {
                    string ageRange = ageEntry.Key;
                    if(currentAgeBracket == ageRange)
                    {
                        if(question == Constants.Question1 && smoker)
                        {
                            result += ageEntry.Value;
                        }
                        if (question == Constants.Question2 && drinker)
                        {
                            result += ageEntry.Value;
                        }
                        if (question == Constants.Question3 && !exercise)
                        {
                            result += ageEntry.Value;
                        }

                    }

                }
            }

            return Task.FromResult(result);
        }

        public Task<int> GetRisk(bool smoker, bool drinker, bool exercise, DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }
    }
}
