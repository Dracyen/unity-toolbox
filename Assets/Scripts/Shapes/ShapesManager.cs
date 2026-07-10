using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShapesManager : MonoBehaviour
{
    [Header("References:")]
    public Transform SpawnerX;
    public Transform SpawnerZ;
    public LayerMover Prefab;

    [Space, Header("Adjustable Values:")]
    public float Speed = 1;

    [Tooltip("Doubles as starting Size")]
    public float WidthInX = 10;

    [Tooltip("Doubles as starting Size")]
    public float WidthInZ = 10;
    public float LayerHeight = 0.5f;
    public float LayerRoundValue = 0.5f;
    public float SpawnerDistance = 15f;


    /// Private Values
    private ShapesControls shapesControls;

    private Transform[] Spawners;

    private List<GameObject> LayerList;

    private float startingScaleX;
    private float startingScaleZ;
    private float startingHeight;

    private LayerDirection currentDirection;

    private LayerMover currentLayer;

    private int currentLevel;

    private Coroutine MovementRoutine;

    private Vector3 centerPos;

    private bool Initialized = false;

    private static ShapesManager shapesManager;

    public static ShapesManager Instance
    {
        get
        {
            if (!shapesManager)
            {
                shapesManager = FindObjectOfType(typeof(ShapesManager)) as ShapesManager;

                if (!shapesManager)
                {
                    Debug.LogError("There needs to be one active ShapesManager script on a GameObject in your scene.");
                }
            }

            return shapesManager;
        }
    }

    private void Awake()
    {
        shapesControls = new ShapesControls();

        Spawners = new Transform[] { SpawnerX, SpawnerZ };

        shapesControls.Shapes.Place.performed += ctx => PlaceLayer(ctx);

        shapesControls.Shapes.Enable();

        LayerList = new List<GameObject>();

        startingScaleZ = WidthInZ;

        startingScaleX = WidthInX;

        startingHeight = transform.position.y;

        centerPos = transform.position;
    }

    public void InitializeGame()
    {
        LayerList.Clear();

        WidthInZ = startingScaleZ;

        WidthInX = startingScaleX;

        transform.position = new Vector3(0, startingHeight, 0);

        currentDirection = LayerDirection.X;

        currentLevel = 1;

        AddLevel();

        Transform spawner = transform;

        currentLayer = Instantiate(Prefab, spawner.position, new Quaternion());

        LayerList.Add(currentLayer.gameObject);

        Vector3 scale = new Vector3(WidthInX, LayerHeight, WidthInZ);

        float distance = 0;

        currentLayer.Initialize(scale, Speed, -distance, currentDirection, out MovementRoutine);

        SpawnLayer();

        Initialized = true;
    }

    private void Update()
    {
        if(Initialized)
        {
            GameLogic();
        }
    }

    private void GameLogic()
    {
        
    }

    private void AddLevel()
    {
        Vector3 movement = new Vector3(0, LayerHeight, 0);

        transform.Translate(movement);

        currentLevel++;
    }

    private void SpawnLayer()
    {
        AddLevel();

        Transform spawner = Spawners[(int)currentDirection].transform;

        currentLayer = Instantiate(Prefab, spawner.position, new Quaternion());

        LayerList.Add(currentLayer.gameObject);

        Vector3 scale = new Vector3(WidthInX, LayerHeight, WidthInZ);

        float distance = 0;

        switch(currentDirection)
        {
            case LayerDirection.X:
                distance = centerPos.x + WidthInX;
            break;

            case LayerDirection.Z:
                distance = centerPos.z + WidthInZ;
            break;
        }

        currentLayer.Initialize(scale, Speed, -distance, currentDirection, out MovementRoutine);
    }

    private void PlaceLayer(InputAction.CallbackContext context)
    {
        if (!Initialized)
            return;

        StopCoroutine(MovementRoutine);

        PlacementLogic();

        CutLogic();

        SetSpawnerLocation();

        if (!Initialized)
            return;

        SwitchDirection();

        SpawnLayer();
    }

    private void PlacementLogic()
    {
        float target = currentLayer.transform.position.x;

        if (currentDirection == LayerDirection.Z)
            target = currentLayer.transform.position.z;

        float fixedPos = MyMath.MRound(target, LayerRoundValue);

        Vector3 newPos = currentLayer.transform.position;

        switch (currentDirection)
        {
            case LayerDirection.X:
                newPos.x = fixedPos;
                break;

            case LayerDirection.Z:
                newPos.z = fixedPos;
                break;
        }

        currentLayer.transform.position = newPos;
    }

    private void SetSpawnerLocation()
    {
        Vector3 pos = Spawners[(int)LayerDirection.X].localPosition;

        pos.x = SpawnerDistance;
        pos.z = centerPos.z;

        Spawners[(int)LayerDirection.X].localPosition = pos;

        pos.z = SpawnerDistance;
        pos.x = centerPos.x;

        Spawners[(int)LayerDirection.Z].localPosition = pos;
    }

    private void CutLogic()
    {
        float currentWidthValue = WidthInX;

        float layerToCenterDistance = currentLayer.transform.position.x - centerPos.x;

        if (currentDirection == LayerDirection.Z)
        {
            currentWidthValue = WidthInZ;

            layerToCenterDistance = currentLayer.transform.position.z - centerPos.z;
        }

        float absValue = Mathf.Abs(layerToCenterDistance);

        if (absValue >=  currentWidthValue)
        {
            GameOver();
        }
        else if(absValue == 0)
        {
            //Perfect
            Debug.Log("Perfect");
        }
        else
        {
            Debug.Log("Has to Cut - " + layerToCenterDistance + " / " + currentWidthValue + " / " + absValue);

            bool isNegative = false;

            if (layerToCenterDistance < 0)
                isNegative = true;
            
            float newSize = currentWidthValue - absValue;

            float newPos = currentWidthValue / 2 - newSize / 2;

            if (isNegative)
                newPos = -newPos;

            Debug.Log("Distance was " + layerToCenterDistance + " therefore the new size is " + newSize + " and it's position is " + newPos);

            Vector3 temp = currentLayer.transform.localScale;

            switch (currentDirection)
            {
                case LayerDirection.X:

                    WidthInX = newSize;

                    temp.x = newSize;

                    currentLayer.transform.localScale = temp;

                    temp = currentLayer.transform.position;

                    temp.x = newPos + centerPos.x;

                    currentLayer.transform.position = temp;

                    break;

                case LayerDirection.Z:

                    WidthInZ = newSize;

                    temp.z = newSize;

                    currentLayer.transform.localScale = temp;

                    temp = currentLayer.transform.position;

                    temp.z = newPos + centerPos.z;

                    currentLayer.transform.position = temp;

                    break;
            }

            centerPos = temp;

            //GameOver();
        }
    }

    private void SwitchDirection()
    {
        LayerDirection temp = LayerDirection.X;

        if (currentDirection == LayerDirection.X)
            temp = LayerDirection.Z;

        currentDirection = temp;
    }

    public void GameOver()
    {
        Initialized = false;
    }
}