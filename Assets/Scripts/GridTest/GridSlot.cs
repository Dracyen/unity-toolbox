using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSlot
{
    public bool full;

    public bool[,] SubGrid;

    public bool subGridInitiated { get; private set; }

    public GridSlot()
    {
        int chance = Random.Range(0, 10);

        if (chance > 7)
            full = true;
        else
            full = false;
    }

    public void InitializeSubGrid(Vector2Int SubGridSize)
    {
        if(!subGridInitiated)
        {
            SubGrid = new bool[SubGridSize.x, SubGridSize.y];

            for (int i = 0; i < SubGridSize.x; i++)
            {
                for (int j = 0; j < SubGridSize.y; j++)
                {
                    int chance = Random.Range(0, 10);

                    if (chance > 7)
                        SubGrid[i, j] = true;
                    else
                        SubGrid[i, j] = false;
                }
            }

            subGridInitiated = true;
        }
    }
}
