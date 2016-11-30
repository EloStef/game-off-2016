using UnityEngine;
using System;

public class Score
{
    private int points = 0;
    private int highScore = 0;

    public Score(int points)
    {
        this.points = points;
        this.highScore = PlayerPrefs.GetInt("High Score");
    }

    public void Update()
    {

    }

    /// <summary>
    /// add/remove points to current score
    /// </summary>
    public void addPoints(int amount)
    {
        points += amount;
    }

    /// <summary>
    /// return string of points in min 3 chars
    /// </summary>
    public string getCurrentPoints()
    {
        string return_ = points.ToString();
        int end = 3 - return_.Length;
        for (int i = 0; i < end; i++)
        {
            return_ = "0" + return_;
        }
        return return_;
    }

    public static int getBestScore()
    {
        return PlayerPrefs.GetInt("High Score");
    }

    public void saveIfBestScore()
    {
        if (points > PlayerPrefs.GetInt("High Score"))
            PlayerPrefs.SetInt("High Score", points);
    }
}