using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs;

    public float interval;

    public float distance = 50;

    public float speed = 0;

    public Timer timer;

    private bool spawning;

    private static ObstacleManager obstacleManager;

    public static ObstacleManager Instance
    {
        get
        {
            if (!obstacleManager)
            {
                obstacleManager = FindObjectOfType(typeof(ObstacleManager)) as ObstacleManager;

                if (!obstacleManager)
                {
                    Debug.LogError("There needs to be one active ObstacleManager script on a GameObject in your scene.");
                }
            }

            return obstacleManager;
        }
    }

    private void Start()
    {
        spawning = false;

        timer.SetupTimer(interval);

        Initialize();
    }

    private void Initialize()
    {
        timer.StartTimer();
    }

    private void Update()
    {
        if(timer.Finished && !spawning)
        {
            spawning = true;

            SpawnObstacles();

            timer.SetupTimer(interval);

            timer.StartTimer();

            spawning = false;
        }
    }

    private void SpawnObstacles()
    {
        var lanes = LaneManager.Instance.lanes;

        double remainingLanes = lanes.Length;

        double obstacleQuantity = 0;

        switch (Random.Range(0, 101))
        {
            case int n when (n > 10 && n <= 30): //1 obstacles
                obstacleQuantity = 1;
                break;

            case int n when (n > 30 && n <= 70): //2 obstacles
                obstacleQuantity = 2;
                break;

            case int n when (n > 70 && n <= 100): //3 obstacles
                obstacleQuantity = 3;
                break;
        }

        double chance = 0f;

        List<ObstacleMovement> obstacles = new List<ObstacleMovement>();

        foreach (var lane in lanes)
        {
            chance = obstacleQuantity / remainingLanes;

            Debug.Log("Chance: " + chance.ToString("F2") + " = " + obstacleQuantity + " + " + remainingLanes);

            if (Random.Range(0f, 1f) <= chance)
            {
                var temp = Instantiate(ObstaclePrefabs[Random.Range(0, ObstaclePrefabs.Length)]);

                obstacles.Add(temp.GetComponent<ObstacleMovement>());

                Vector3 pos = temp.transform.position;

                pos.x = lane.position.x;

                pos.z = distance;

                temp.transform.position = pos;

                obstacleQuantity--;
            }

            remainingLanes--;
        }

        foreach (var obstacle in obstacles)
        {
            obstacle.Initialize(speed);
        }
    }
}