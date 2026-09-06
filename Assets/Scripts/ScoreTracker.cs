public class ScoreTracker
{
    public int Score { get; set; }
    public int MaxScore { get; private set; }

    public ScoreTracker(EnemyPlaceholder[] levelEnemies)
    {
        foreach (EnemyPlaceholder enemy in levelEnemies)
        {
            MaxScore += enemy.Data.ScorePoints;
        }
    }

    public int CalculateRating()
    {
        float scorePercentage = (float)Score / MaxScore * 100;

        return scorePercentage switch
        {
            >= 100  => 3,
            >= 75   => 2,
            >= 50   => 1,
            _       => 0
        };
    }
}
