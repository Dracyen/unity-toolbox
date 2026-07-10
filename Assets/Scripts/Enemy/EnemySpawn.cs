using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int Quantity = 0;

    public GameObject Enemy;

    public Transform Spawnpoint;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for(int i = 0; i < Quantity; i++)
        {
            Instantiate(Enemy, Spawnpoint);
        }
    }
}
