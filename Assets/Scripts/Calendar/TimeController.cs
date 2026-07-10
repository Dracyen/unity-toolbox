using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public CalendarManager GameCalendar;

    private static TimeController timeController;

    [Range(0.1f, 2.0f)]
    public float TimeScale;

    public static TimeController Instance
    {
        get
        {
            if (!timeController)
            {
                timeController = FindObjectOfType(typeof(TimeController)) as TimeController;

                if (!timeController)
                {
                    Debug.LogError("There needs to be one active TimeController script on a GameObject in your scene.");
                }
            }

            return timeController;
        }
    }

    public event Action OnUpdate;
    public event Action OnTimeResume;
    public event Action OnTimePause;

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    PauseTime();
        //}

        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    ResumeTime();
        //}
    }

    void PauseTime()
    {
        Debug.Log("Pausing");
        OnTimePause?.Invoke();
    }

    void ResumeTime()
    {
        Debug.Log("Resuming");
        OnTimeResume?.Invoke();
    }
}
