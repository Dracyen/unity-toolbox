using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BasePlayerControls playerControls;

    public Camera playerCamera;

    private void Awake()
    {
        playerControls = new BasePlayerControls();

        playerControls.Player.Move.performed += ctx => Move(ctx);
        playerControls.Player.Look.performed += ctx => Look(ctx);
        playerControls.Player.Fire.performed += ctx => Fire(ctx);

        playerControls.Enable();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
    }

    public void Look(InputAction.CallbackContext context)
    {
        context.ReadValue<Vector2>();

        //Debug.Log("Looking");
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }
}
