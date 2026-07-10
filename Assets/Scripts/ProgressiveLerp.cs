using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressiveLerp
{
    public Vector3 TopValue { get; private set; }

    public Vector3 BotValue { get; private set; }

    public float rate;

    public float Progress { get; private set; }

    public Vector3 Value { get; private set; }

    public bool atTop;

    public bool atBot;

    public ProgressiveLerp(float BotValue = 1, float TopValue = 1, float rate = 10f)
    {
        atTop = false;
        atBot = false;

        this.TopValue = new Vector3(TopValue, 0, 0);
        this.BotValue = new Vector3(BotValue, 0, 0);
        this.rate = rate;
        Progress = 0;
        Value = new Vector3(BotValue, 0, 0);
    }

    public ProgressiveLerp(Vector3 BotValue = new Vector3(), Vector3 TopValue = new Vector3(), float rate = 10f)
    {
        atTop = false;
        atBot = false;

        this.TopValue = TopValue;
        this.BotValue = BotValue;
        this.rate = rate;
        Progress = 0;
        Value = BotValue;
    }

    public void Update()
    {
        Value = Vector3.Lerp(BotValue, TopValue, Progress);

        Progress += rate * 100;
    }
}
