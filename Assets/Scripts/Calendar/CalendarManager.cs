using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public float IncrementTime = 10;

    public Text calendarText;

    bool Updating;

    Calendar currentCalendar;

    float timer;

    void Awake()
    {
        TimeController.Instance.GameCalendar = this;

        TimeController.Instance.OnTimeResume += ResumeUpdate;
        TimeController.Instance.OnTimePause += PauseUpdate;

        currentCalendar = new Calendar();

        timer = 0;

        Updating = true;
    }

    private void Update()
    {
        if(Updating)
        {
            if (timer > IncrementTime)
            {
                timer = 0;

                currentCalendar.TimeTick();

                calendarText.text = (currentCalendar.Clock.CurrentClock() + " - " + currentCalendar.Date.CurrentDate());
            }
            else
            {
                timer += UnityEngine.Time.deltaTime;
            }
        }
    }

    void PauseUpdate()
    {
        Updating = false;
    }

    void ResumeUpdate()
    {
        Updating = true;
    }

    private void OnDestroy()
    {
        TimeController.Instance.OnTimeResume -= ResumeUpdate;
        TimeController.Instance.OnTimePause -= PauseUpdate;
    }
}

public class Calendar
{
    public Date Date;

    public Clock Clock;

    public Calendar()
    {
        Date = new Date();

        Clock = new Clock();
    }

    public void TimeTick()
    {
        Clock.AddSeconds();

        TimeLogic();
    }

    void TimeLogic()
    {
        if(Clock.Seconds >= 60)
        {
            Clock.SetSeconds(0);
            Clock.AddMinutes();
        }

        if (Clock.Minutes >= 60)
        {
            Clock.SetMinutes(0);
            Clock.AddHours();
        }

        if (Clock.Hours >= 24)
        {
            Clock.SetHours(0);
            Date.AddDays();
        }

        if (Date.Days >= 30)
        {
            Date.SetDays(0);
            Date.AddMonths();
        }

        if (Date.Months >= 12)
        {
            Date.SetMonths(0);
            Date.AddYears();
        }
    }
}

public class Date
{
    public int Days { get; private set; }

    public int Months { get; private set; }

    public int Years { get; private set; }

    public string CurrentDate()
    {
        return (Days.ToString() + "/" + Months.ToString() + "/" + Years.ToString());
    }

    public void SetDate(int d, int m, int y)
    {
        SetDays(d);
        SetMonths(m);
        SetYears(y);
    }

    public void SetDays(int i)
    {
        Days = i;
    }

    public void SetMonths(int i)
    {
        Months = i;
    }

    public void SetYears(int i)
    {
        Years = i;
    }
    public void AddDays(int i = 1)
    {
        Days += i;
    }

    public void AddMonths(int i = 1)
    {
        Months += i;
    }

    public void AddYears(int i = 1)
    {
        Years += i;
    }
}

public class Clock
{
    public int Hours { get; private set; }

    public int Minutes { get; private set; }

    public int Seconds { get; private set; }

    public string CurrentClock()
    {
        return (Hours.ToString() + ":" + Minutes.ToString() + ":" + Seconds.ToString());
    }
    
    public void SetTime(int h, int m, int s)
    {
        SetHours(h);
        SetMinutes(m);
        SetSeconds(s);
    }

    public void SetHours(int i)
    {
        Hours = i;
    }

    public void SetMinutes(int i)
    {
        Minutes = i;
    }

    public void SetSeconds(int i)
    {
        Seconds = i;
    }

    public void AddHours(int i = 1)
    {
        Hours += i;
    }

    public void AddMinutes(int i = 1)
    {
        Minutes += i;
    }

    public void AddSeconds(int i = 1)
    {
        Seconds += i;
    }
}
