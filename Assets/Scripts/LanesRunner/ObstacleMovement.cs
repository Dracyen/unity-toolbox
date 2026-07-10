using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public int weight = 0;

    public Direction direction;

    public float Speed;

    private Vector3 _direction;

    private bool initialized = false;

    public void Initialize(float speed = 0)
    {
        switch (direction)
        {
            case Direction.NORTH: _direction = Vector3.forward; break;
            case Direction.SOUTH: _direction = -Vector3.forward; break;
            case Direction.EAST: _direction = Vector3.right; break;
            case Direction.WEST: _direction = -Vector3.right; break;
        }

        if(speed > 0)
            Speed = speed;

        initialized = true;
    }

    private void Update()
    {
        if (initialized)
            Move();
    }

    private void Move()
    {
        transform.position += _direction * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Deadline"))
        {
            Destroy(gameObject);
        }
    }
}
