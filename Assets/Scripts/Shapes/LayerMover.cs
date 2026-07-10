using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerMover : MonoBehaviour
{
    [SerializeField]
    float TargetPosition;

    float Speed;

    LayerDirection TargetDirection;

    public void Initialize(Vector3 scale, float speed, float target, LayerDirection direction, out Coroutine routine)
    {
        TargetPosition = target;

        TargetDirection = direction;

        transform.localScale = scale;

        Speed = speed;

        routine = StartCoroutine(MoveLayer());
    }

    private bool LayerLogic()
    {
        return true;
    }

    private IEnumerator MoveLayer()
    {
        Vector3 movement = new Vector3(0, 0, Speed);

        float position = transform.position.z;

        if (TargetDirection == LayerDirection.X)
        {
            movement = Vector3.zero;
            movement.x = Speed;
            position = transform.position.x;
        }

        while (TargetPosition < position)
        {
            transform.position -= movement * Time.deltaTime;
            yield return new WaitForEndOfFrame();

            position = transform.position.z;

            if (TargetDirection == LayerDirection.X)
                position = transform.position.x;
        }

        ShapesManager.Instance.GameOver();
    }
}
