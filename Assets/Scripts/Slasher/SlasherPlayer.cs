using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlasherPlayer : MonoBehaviour
{
    public float playerY = 1;

    public LayerMask layerMask;

    public float Cooldown = 0.2f;

    public Timer timer;

    private SlasherControls slasherControls;

    private bool pressed = false;

    private void Awake()
    {
        slasherControls = new SlasherControls();

        slasherControls.Slasher.Action.started += ctx => StartAction(ctx);
        slasherControls.Slasher.Action.performed += ctx => Slash(ctx);
        slasherControls.Slasher.Action.canceled += ctx => CancelAction(ctx);

        slasherControls.Slasher.Enable();
    }

    private void Start()
    {
        timer.SetupTimer(Cooldown);
        timer.StartTimer();
    }

    void StartAction(InputAction.CallbackContext ctx)
    {
        pressed = true;
    }

    void CancelAction(InputAction.CallbackContext ctx)
    {
        pressed = false;
    }

    void Slash(InputAction.CallbackContext ctx)
    {
        if(pressed)
        {
            Debug.Log("Hold");
        }
        else
        {
            if (timer.Finished)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit, 200, layerMask))
                {
                    //Debug.Log("Hit Layer: " + hit.transform.gameObject.layer);
                    //Debug.Log("Hit Name: " + hit.transform.gameObject.name);

                    EnemyEntity target = hit.transform.GetComponent<EnemyEntity>();

                    Vector3 oldPos = transform.position;

                    Vector3 newPos = hit.point;

                    newPos.y = playerY;

                    transform.position = newPos;

                    Debug.DrawLine(transform.position, hit.point, Color.red, 0);
                }

                timer.StartTimer();
            }
        }
    }
}
