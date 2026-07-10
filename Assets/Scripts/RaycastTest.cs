using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public LayerMask layerMask;

    public int Damage = 100;

    public GameObject hitVFX;

    public int vfxCount = 10;

    List<GameObject> hitVFXList;

    private void Awake()
    {
        //for(int i = 0; i < vfxCount; i++)
        //    hitVFXList.Add(Instantiate(hitVFX));
    }

    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hit = new RaycastHit();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if(Physics.Raycast(ray, out hit, 50, layerMask))
        //    {
        //        Debug.Log("Hit Layer: " + hit.transform.gameObject.layer);
        //        Debug.Log("Hit Name: " + hit.transform.gameObject.name);

        //        EnemyEntity target = hit.transform.GetComponent<EnemyEntity>();

        //        //StartCoroutine(PlayVFX());

        //        Instantiate(hitVFX, hit.point, new Quaternion());

        //        target.DamageHealth(Damage);
        //    }
        //}
    }

    //IEnumerator PlayVFX()
    //{
    //    Instantiate()
    //}
}