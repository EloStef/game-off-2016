using UnityEngine;
using System;

public class Score
{
    private int points = 0;

    public Score(int points)
    {
        this.points = points;
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

    public void getBestScore()
    {

    }

    public void saveIfBestScore()
    {

    }
}