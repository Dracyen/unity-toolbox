using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMath
{
    public static float MRound(float value, float round)
    {
        bool isNegative = false;

        if(value < 0)
        {
            isNegative = true;

            value = Mathf.Abs(value);
        }

        float previous = 0;

        float next = round;

        while (next < value)
        {
            previous = next;

            next = next + round;
        }

        float value1 = Mathf.Abs(value - previous);

        float value2 = Mathf.Abs(value - next);

        float endValue = previous;

        if (value2 < value1)
            endValue = next;

        if(isNegative)
            return -endValue;

        return endValue;
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angles)
    {
        Vector3 dir = point - pivot;
        dir = angles * dir;
        point = dir + pivot;
        return point;
    }
}
