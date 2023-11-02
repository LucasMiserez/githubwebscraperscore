using linkedinwebscraper.modules;

namespace linkedinwebscraper;

public class Score
{
  
    public double CalculateScore(User user)
    {
        int score = 0;
        if (!string.IsNullOrEmpty(user.fullName))
        {
            score += 10;
        }
        if (!string.IsNullOrEmpty(user.bio))
        {
            score += 10;
        }
        if (!string.IsNullOrEmpty(user.location))
        {
            score += 5;
        }

        if (user.readme)
        {
            score += 10;
        }
        switch (user.repositories)
        {
            case   > 8:
                score += 15;
                break;
            case > 2:
                score += 10;
                break;
        }
        switch (user.stars)
        {
            case   > 5:
                score += 15;
                break;
            case > 1:
                score += 10;
                break;
        }
        switch (user.social.Length)
        {
            case   > 1:
                score += 10;
                break;
            case > 0:
                score += 5;
                break;
        }
        switch (user.contributions)
        {
            case > 2000:
                score += 20;
                break;
            case > 1000:
                score += 15;
                break;
            case > 500:
                score += 10;
                break;
            case > 100:
                score += 5;
                break;
            case > 10:
                score += 3;
                break;
        }
        if (user.pinned)
        {
            score += 10;
        }
        if (user.achievements)
        {
            score += 10;
        }
        if (user.highlights)
        {
            score += 5;
        }
        if (user.sponsors)
        {
            score += 10;
        }
        if (user.organizations)
        {
            score += 5;
        }

        double scoreCal = Math.Round((score / 1.15), 2);
        return scoreCal > 100 ? 100 : scoreCal;
    }
}