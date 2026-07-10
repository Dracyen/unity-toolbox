using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetBool("Fly", true);
        else if(Input.GetKeyUp(KeyCode.Space))
            anim.SetBool("Fly", false);
    }
}
