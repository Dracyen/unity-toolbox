using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LockPick : MonoBehaviour
{
    //Lockpick Position
    //Unlock Position

    [SerializeField] private Transform Lock;
    [SerializeField] private Transform Pick;

    public int[] sweetSpot;

    int sweetSpotRange;

    public float pickPosition;

    public int roundedPickPosition;
    
    int keyHoleMax = 50;
    int keyHoleMin = -50;

    CustomAxis unlockProgression;

    private void Awake()
    {
        Setup(LockPickDifficulty.HARD);
    }

    private void Setup(LockPickDifficulty x = LockPickDifficulty.EASY)
    {
        pickPosition = 0;

        roundedPickPosition = 0;

        sweetSpotRange = (int)x;

        sweetSpot = new int[sweetSpotRange];

        unlockProgression = new CustomAxis(0, 0, 10, 0);

        int midRange = Mathf.RoundToInt(sweetSpotRange / 2);

        int center = Random.Range(keyHoleMin, keyHoleMax + 1);

        if(center - midRange < keyHoleMin)
        {
            int startingPoint = keyHoleMin;

            for(int i = 0; i < sweetSpotRange; i++)
            {
                sweetSpot[i] = startingPoint;
                startingPoint++;
            }
        }
        else if(center + midRange > keyHoleMax)
        {
            int startingPoint = keyHoleMax;

            for (int i = 0; i < sweetSpotRange; i++)
            {
                sweetSpot[i] = startingPoint;
                startingPoint--;
            }
        }
        else
        {
            int startingPoint = center - midRange;

            for (int i = 0; i < sweetSpotRange; i++)
            {
                sweetSpot[i] = startingPoint;
                startingPoint++;
            }
        }
    }

    public void Update()
    {
        //pickPosition += Input.GetAxis("Mouse X");

        roundedPickPosition = Mathf.RoundToInt(pickPosition);

        if (roundedPickPosition > keyHoleMax) roundedPickPosition = keyHoleMax;
        else if (roundedPickPosition < keyHoleMin) roundedPickPosition = keyHoleMin;

        Pick.eulerAngles = new Vector3(Pick.eulerAngles.x, Pick.eulerAngles.y, roundedPickPosition);

        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    AttemptUnlock();
        //}

        if (pickPosition > keyHoleMax) pickPosition = keyHoleMax;
        if (pickPosition < keyHoleMin) pickPosition = keyHoleMin;
    }

    void AttemptUnlock()
    {

    }

    void Unlock()
    {
        if(sweetSpot.Contains(roundedPickPosition))
        {
            Debug.Log("Unlocked!!");
        }
        else
        {
            Debug.Log("Not Yet - " + Mathf.Abs((float)roundedPickPosition - (float)sweetSpot.Average()));
        }
    }
}
