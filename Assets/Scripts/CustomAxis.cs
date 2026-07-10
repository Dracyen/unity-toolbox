using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAxis
{
    public float TopValue { get; private set; }

    public float BotValue { get; private set; }

    public float rate;

    public float center;

    public float Value { get; private set; }

    public bool atTop;

    public bool atBot;

    public CustomAxis(float BotValue = 1, float TopValue = 1, float rate = 10f, float center = 0)
    {
        atTop = false;
        atBot = false;

        this.TopValue = TopValue;
        this.BotValue = BotValue;
        this.rate = rate;
        this.center = center;
        Value = center;
    }

    public void Update(bool keyBot = false, bool keyTop = false)
    {
        if(Value < center)
        {
            if (keyBot)
            {
                if(Value - rate * Time.deltaTime < BotValue)
                {
                    Value = BotValue;
                }
                else
                {
                    Value -= rate * Time.deltaTime;
                }
            }
            else if (keyTop)
            {
                Value += rate * 2 * Time.deltaTime;
            }
            else
            {
                if (Value + rate * Time.deltaTime > center)
                {
                    Value = center;

                    Debug.Log("Reset from Bottom");
                }
                else
                {
                    Value += rate * Time.deltaTime;
                }
            }
        }
        else if (Value > center)
        {
            if (keyBot)
            {
                Value -= rate * 2 * Time.deltaTime;
            }
            else if (keyTop)
            {
                if(Value + rate * Time.deltaTime > TopValue)
                {
                    Value = TopValue;
                }
                else
                {
                    Value += rate * Time.deltaTime;
                }
            }
            else
            {
                if (Value - rate * Time.deltaTime < center)
                {
                    Value = center;
                    Debug.Log("Reset from Top");
                }
                else
                {
                    Value -= rate * Time.deltaTime;
                }
            }
        }
        else
        {
            if (keyBot)
            {
                Value -= rate * Time.deltaTime;
            }
            else if (keyTop)
            {
                Value += rate * Time.deltaTime;
            }
        }

        if(Value == TopValue)
        {
            atTop = true;
        }
        else
        {
            atTop = false;
        }

        if(Value == BotValue)
        {
            atBot = true;
        }
        else
        {
            atBot = false;
        }
    }

    public void UpdateTopValue(float value)
    {
        TopValue = value;
    }

    public void UpdateBotValue(float value)
    {
        BotValue = value;
    }

    public void UpdateCenter(float value)
    {
        center = value;
    }

    public void UpdateRate(float value)
    {
        rate = value;
    }

    public void ChangeValue(float value)
    {
        Value = value;
    }
}
