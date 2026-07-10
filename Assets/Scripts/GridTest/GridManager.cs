using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int MainGridSize, SubGridSize;

    public Vector2 MainSlotSize, SubSlotSize;

    public GameObject Background, SlotHighlight;

    public LineRenderer gridLineRenderer, borderLineRender, bigBorderLineRender;

    public float BigBorderWidth;

    private static GridManager gridManager;
    public static GridManager Instance
    {
        get
        {
            if (!gridManager)
            {
                gridManager = FindObjectOfType(typeof(GridManager)) as GridManager;

                if (!gridManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
            }

            return gridManager;
        }
    }

    GridSlot[,] MainGrid;

    public void Start()
    {
        Background.transform.localScale = new Vector3(MainGridSize.x * MainSlotSize.x, Background.transform.localScale.y, MainGridSize.y * MainSlotSize.y);

        SlotHighlight.transform.localScale = new Vector3(MainSlotSize.x, SlotHighlight.transform.localScale.y, MainSlotSize.y);

        SubSlotSize = MainSlotSize / SubGridSize;

        MainGrid = new GridSlot[MainGridSize.x, MainGridSize.y];

        for (int i = 0; i < MainGridSize.x; i++)
        {
            for (int j = 0; j < MainGridSize.y; j++)
            {
                MainGrid[i, j] = new GridSlot();
            }
        }

        DrawGrid(MainGridSize.x, MainGridSize.y, MainSlotSize);
    }

    public void MainGridInteract(Vector2Int MainPos)
    {
        MainGrid[MainPos.x, MainPos.y].full = !MainGrid[MainPos.x, MainPos.y].full;
    }

    public void SubGridInteract(Vector2Int MainPos, Vector2Int SubPos)
    {
        if (!MainGrid[MainPos.x, MainPos.y].subGridInitiated)
            MainGrid[MainPos.x, MainPos.y].InitializeSubGrid(SubGridSize);

        MainGrid[MainPos.x, MainPos.y].SubGrid[SubPos.x, SubPos.y] = !MainGrid[MainPos.x, MainPos.y].SubGrid[SubPos.x, SubPos.y];
    }

    public bool CheckSlot(Vector2Int gridPos)
    {
        return MainGrid[gridPos.x, gridPos.y].full;
    }

    public bool CheckSlot(Vector2Int gridPos, Vector2Int subGridPos)
    {
        bool value = false;

        if(MainGrid[gridPos.x, gridPos.y].subGridInitiated)
        {
            value = MainGrid[gridPos.x, gridPos.y].SubGrid[subGridPos.x, subGridPos.y];
        }

        return value;
    }

    public void DrawGrid(int gridSizeX, int gridSizeY, Vector2 cellSize)
    {
        //Calculate the number of points required to create the desired Grid
        gridLineRenderer.positionCount = (gridSizeX * 2 + 1) + (gridSizeY * 2 + 1);
        borderLineRender.positionCount = 5;
        bigBorderLineRender.positionCount = 5;

        //Distribute the points
        int count = 0;

        int target = 0;

        int x, y;

        for (x = 0; x < gridSizeX; x++)
        {
            //Create Line
            gridLineRenderer.SetPosition(count, new Vector3(x, 0, target) * cellSize);
            //Debug.Log("Count: " + count + " - X: " + x + " / Target: " + target);
            count++;

            if (target == gridSizeY)
                target = 0;
            else
                target = gridSizeY;

            gridLineRenderer.SetPosition(count, new Vector3(x, 0, target) * cellSize);
            //Debug.Log("Count: " + count + " - X: " + x + " / Target: " + target);
            count++;
        }

        if (target == gridSizeY)
        {
            gridLineRenderer.SetPosition(count, new Vector3(x - 1, 0, 0) * cellSize);
            //Debug.Log("Count: " + count + " - X: " + x + " / Target: " + target);
            count++;
            target = 0;
        }
        //    _
        // | | | |
        // | | | |
        // |_| |_|

        for (y = 0; y < gridSizeY; y++)
        {
            //Create Line
            gridLineRenderer.SetPosition(count, new Vector3(target, 0, y) * cellSize);
            //Debug.Log("Count: " + count + " - X: " + x + " / Target: " + target);
            count++;

            if (target == gridSizeX)
                target = 0;
            else
                target = gridSizeX;

            gridLineRenderer.SetPosition(count, new Vector3(target, 0, y) * cellSize);
            //Debug.Log("Count: " + count + " - X: " + x + " / Target: " + target);
            count++;
        }

        if (target == gridSizeX)
        {
            gridLineRenderer.SetPosition(count, new Vector3(0, 0, y - 1) * cellSize);
            //Debug.Log("Count: " + count + " - X: " + x + " / Target: " + target);
            count++;
        }
        //  ____
        // |____
        //  ____|
        // |____

        Vector3 pos = Vector3.zero;

        pos = new Vector3(0, 0.1f, 0);
        borderLineRender.SetPosition(0, pos);

        pos.x = gridSizeX * cellSize.x;
        borderLineRender.SetPosition(1, pos);

        pos.z = gridSizeY * cellSize.y;
        borderLineRender.SetPosition(2, pos);

        pos.x = 0;
        borderLineRender.SetPosition(3, pos);

        pos.z = 0;
        borderLineRender.SetPosition(4, pos);
        //  ______
        // |      |
        // |      |
        // |______|

        pos = Vector3.zero;

        pos = new Vector3(-BigBorderWidth, 0.1f, -BigBorderWidth);
        bigBorderLineRender.SetPosition(0, pos);

        pos.x = gridSizeX * cellSize.x + BigBorderWidth;
        bigBorderLineRender.SetPosition(1, pos);

        pos.z = gridSizeY * cellSize.y + BigBorderWidth;
        bigBorderLineRender.SetPosition(2, pos);

        pos.x = -BigBorderWidth;
        bigBorderLineRender.SetPosition(3, pos);

        pos.z = -BigBorderWidth;
        bigBorderLineRender.SetPosition(4, pos);
        //  ________
        // |        |
        // |        |
        // |        |
        // |________|
    }
}
