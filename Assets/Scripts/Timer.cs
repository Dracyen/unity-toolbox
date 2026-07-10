using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float CurrentTime;

    public float time { get; private set; }

    public float targetTime { get; private set; }

    public bool Initialized { get; private set; }

    public bool Finished { get; private set; }

    private void Update()
    {
        if (!Finished && Initialized)
            RunTimer();
    }

    public void StartTimer()
    {
        time = 0;

        CurrentTime = time;

        Initialized = true;

        Finished = false;
    }

    public void SetupTimer(float targetTime = 1)
    {
        this.targetTime = targetTime;
    }

    private void RunTimer()
    {
        if (time < targetTime)
        {
            time += Time.deltaTime;

            CurrentTime = time;
        }
        else
        {
            Finished = true;

            Initialized = false;
        }
    }
}
