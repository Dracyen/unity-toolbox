using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLookAt : MonoBehaviour
{
    public LayerMask layerMask;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, 50, layerMask))
            {
                Debug.Log("Hit Layer: " + hit.transform.gameObject.layer);
                Debug.Log("Hit Name: " + hit.transform.gameObject.name);

                EnemyEntity target = hit.transform.GetComponent<EnemyEntity>();

                transform.LookAt(hit.point);

                Vector3 rot = transform.eulerAngles;

                rot.x = 0;

                rot.z = 0;

                transform.eulerAngles = rot;

                Debug.DrawLine(transform.position, hit.point, Color.red, 0);
            }
        }
    }
}
