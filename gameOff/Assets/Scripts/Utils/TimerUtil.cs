using UnityEngine;
using System;

public class TimerUtil
{
    float timer = 0;
    float timerLimit;

    public TimerUtil(float limit)
    {
        timerLimit = limit;
    }

    public void Update()
    {
        timer += Time.deltaTime;
    }

    public void ChangeLimit(float newLimit)
    {
        timerLimit = newLimit;
    }

    public bool IsLimitReached()
    {
        if (timer >= timerLimit)
        {
            ResetTicks();
            return true;
        }
        return false;
    }
    /// <summary>
    /// reset time counting to 0
    /// </summary>
    public void ResetTicks()
    {
        timer = 0;
    }

    public float getCurrentTime()
    {
        return timer;
    }
    /// <summary>
    /// </summary>
    /// <returns> float in range <0-1></returns>
    public float getCurrentTimeProgress()
    {
        return timer / timerLimit;
    }
    /// <summary>
    /// </summary>
    /// <returns> float in range<1-0></returns>
    public float getCurrentTimeProgressReverse()
    {
        return 1f - (timer / timerLimit);
    }
}