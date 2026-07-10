using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    Ray ray;

    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit[] hits = Physics.RaycastAll(ray);

        //    foreach (RaycastHit hit in hits)
        //    {
        //        if (hit.collider.gameObject.GetComponent<Interactable>() != null)
        //        {
        //            Debug.Log(hit.collider.gameObject.name);
                    
        //            hit.collider.gameObject.GetComponent<Interactable>().Interact();
        //        }
        //    }
        //}
    }
}
