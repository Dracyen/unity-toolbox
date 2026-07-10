using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Transform t1;

    public Transform t2;
    
    public int interpolationFramesCount = 45;

    public int elapsedFrames = 0;

    void Update()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        transform.position = Vector3.Lerp(t1.position, t2.position, interpolationRatio);

        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
    }
}
