using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliderUI : MonoBehaviour
{
    public RectTransform IndicatorCenter;
    public float MinAngle = -20;
    public float MaxAngle = -115;

    public void UpdateSlider(float value, float maxValue)
    {
        Vector3 rot = Vector3.zero;

        float diference = -MinAngle;

        float fakeMin = MinAngle + diference;
        float fakeMax = MaxAngle + diference;

        Debug.Log("Diference: " + diference + " - Min/Max: " + fakeMin + " / " + fakeMax);

        float tempRot = value * (fakeMax) / maxValue;

        rot.z = tempRot - diference;

        IndicatorCenter.rotation = Quaternion.Euler(rot);
    }
} 
