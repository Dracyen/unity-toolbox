using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public Transform[] lanes;

    private static LaneManager laneManager;

    public static LaneManager Instance
    {
        get
        {
            if (!laneManager)
            {
                laneManager = FindObjectOfType(typeof(LaneManager)) as LaneManager;

                if (!laneManager)
                {
                    Debug.LogError("There needs to be one active LaneManger script on a GameObject in your scene.");
                }
            }

            return laneManager;
        }
    }
}
