using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementScript : MonoBehaviour
{
    public GameObject Player;

    private Vector3[,] MapPos = new Vector3[5, 5];
    private int[,] MapState;

    private int[,] OrgMapState;

    private int PosX = 0;
    private int PosY = 0;

    private void Awake()
    {
        OrgMapState = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
    }

    void Start()
    {
        ResetMap(0, 0);

        //RenderPos();
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    Debug.Log("Trying X: " + (PosX - 1) + " and Y: " + PosY);

        //    Move(PosX - 1, PosY);
        //}

        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    Debug.Log("Trying X: " + PosX + " and Y: " + (PosY - 1));

        //    Move(PosX, PosY - 1);
        //}

        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    Debug.Log("Trying X: " + (PosX + 1) + " and Y: " + PosY);

        //    Move(PosX + 1, PosY);
        //}

        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    Debug.Log("Trying X: " + PosX + " and Y: " + (PosY + 1));

        //    Move(PosX, PosY + 1);
        //}

        //if (Input.GetKeyUp(KeyCode.M))
        //{
        //    Debug.Log(MapState[0, 0] + " - " + MapState[0, 1] + " - " + MapState[0, 2] + " - " + MapState[0, 3] + " - " + MapState[0, 4]);
        //    Debug.Log(MapState[1, 0] + " - " + MapState[1, 1] + " - " + MapState[1, 2] + " - " + MapState[1, 3] + " - " + MapState[1, 4]);
        //    Debug.Log(MapState[2, 0] + " - " + MapState[2, 1] + " - " + MapState[2, 2] + " - " + MapState[2, 3] + " - " + MapState[2, 4]);
        //    Debug.Log(MapState[3, 0] + " - " + MapState[3, 1] + " - " + MapState[3, 2] + " - " + MapState[3, 3] + " - " + MapState[3, 4]);
        //    Debug.Log(MapState[4, 0] + " - " + MapState[4, 1] + " - " + MapState[4, 2] + " - " + MapState[4, 3] + " - " + MapState[0, 4]);
        //}
    }

    void ResetMap(int x, int y)
    {
        MapState = OrgMapState;

        Debug.Log("Is Reset.");

        MapState[x, y] = 2;
    }

    void RenderPos()
    {
        Player.transform.position = MapPos[PosX, PosY];
    }

    void Move(int x, int y)
    {
        int tgtPosX = x;
        int tgtPosY = y;

        if (CheckTile(tgtPosX, tgtPosY))
        {
            PosX = tgtPosX;
            PosY = tgtPosY;

            ResetMap(PosX, PosY);

            //RenderPos();
        }

        Debug.Log("Coord X: " + PosX + " / Coord Y: " + PosY);
    }

    bool CheckTile(int x, int y)
    {
        bool value;

        Debug.Log("The map state is:" + MapState[x, y]);

        switch (MapState[x, y])
        {
            case 0:
                value = true;
                break;

            case 1:
                value = false;
                break;

            default:
                value = false;
                break;
        }

        return value;
    }
}
