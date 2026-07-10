using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanesPlayerController : MonoBehaviour
{
    private Lane currentLane;

    private LanesControls lanesControls;

    private bool movingHorizontally;

    private bool movingVertically;

    private bool crouched;

    public GameObject Standing;

    public GameObject Crouching;

    public int interpolationFramesCount = 45;

    public float crouchTime = 1;

    public float jumpForce = 45;

    private float actualGravity;

    public float normalGravity = 1;

    public float quickLandingGravity = 10;

    public float jumpMultiplier = 0.1f;

    private void Awake()
    {
        lanesControls = new LanesControls();

        lanesControls.Gameplay.MovementUp.performed += ctx => MoveUp(ctx);

        lanesControls.Gameplay.MovementDown.performed += ctx => MoveDown(ctx);

        lanesControls.Gameplay.MovementLeft.performed += ctx => MoveLeft(ctx);

        lanesControls.Gameplay.MovementRight.performed += ctx => MoveRight(ctx);

        Initialize();
    }

    private void Initialize()
    {
        Crouching.SetActive(false);
        lanesControls.Gameplay.Enable();
        currentLane = Lane.CENTER;
        movingHorizontally = false;
        movingVertically = false;
        crouched = false;
        actualGravity = normalGravity;
    }

    private void MoveUp(InputAction.CallbackContext ctx)
    {
        if(!movingVertically)
            StartCoroutine(Jump());
    }

    private void MoveDown(InputAction.CallbackContext ctx)
    {
        if(movingVertically)
        {
            StartCoroutine(QuickLanding());
        }
        else
        {
            if(!crouched)
                StartCoroutine(Crouch());
        }
    }

    private IEnumerator QuickLanding()
    {
        while (movingVertically)
        {
            actualGravity = quickLandingGravity;

            yield return new WaitForEndOfFrame();
        }

        actualGravity = normalGravity;
    }

    private IEnumerator Crouch()
    {
        Crouching.SetActive(true);
        Standing.SetActive(false);

        crouched = true;

        for (float timer = crouchTime; timer >= 0; timer -= Time.deltaTime)
        {
            if(movingVertically)
            {
                Uncrouch();
                yield return null;
            }

            yield return new WaitForEndOfFrame();
        }

        Uncrouch();
    }

    public void Uncrouch()
    {
        Crouching.SetActive(false);
        Standing.SetActive(true);

        crouched = false;
    }

    private void MoveLeft(InputAction.CallbackContext ctx)
    {
        if(!movingHorizontally)
        {
            if(currentLane - 1 >= 0)
            {
                StartCoroutine(MoveHor(currentLane - 1));
            }
        }
    }

    private void MoveRight(InputAction.CallbackContext ctx)
    {
        if (!movingHorizontally)
        {
            if ((int)currentLane + 1 <= LaneManager.Instance.lanes.Length - 1)
            {
                StartCoroutine(MoveHor(currentLane + 1));
            }
        }
    }

    private IEnumerator Jump()
    {
        movingVertically = true;

        float originalPos = transform.position.y;

        Vector3 acceleration = Vector3.zero;

        acceleration.y += jumpForce * jumpMultiplier;

        transform.position += acceleration;

        while (transform.position.y >= originalPos)
        {
            acceleration.y -= actualGravity * jumpMultiplier;

            transform.position += acceleration;

            yield return new WaitForEndOfFrame();
        }

        acceleration = transform.position;

        acceleration.y = originalPos;

        transform.position = acceleration;

        movingVertically = false;
    }

    private IEnumerator MoveHor(Lane target)
    {
        movingHorizontally = true;

        LaneManager laneMngr = LaneManager.Instance;

        float t1 = laneMngr.lanes[(int)currentLane].position.x;

        float t2 = laneMngr.lanes[(int)target].position.x;

        int elapsedFrames = 0;

        float interpolationRatio;

        Vector3 Pos = Vector3.zero;

        while (elapsedFrames != interpolationFramesCount)
        {
            interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

            Pos.x = Mathf.Lerp(t1, t2, interpolationRatio);

            Pos.y = transform.position.y;

            Pos.z = transform.position.z;

            transform.position = Pos;

            int oldFrames = elapsedFrames;

            elapsedFrames = (oldFrames + 1) % (interpolationFramesCount + 1);

            //Debug.Log("Elapsed Frames: " + elapsedFrames + " = OldFrames: " + oldFrames + " + 1 % interpolationFramesCount: " + interpolationFramesCount + " + 1");

            yield return new WaitForEndOfFrame();
        }

        Pos.x = t2;

        transform.position = Pos;

        currentLane = target;

        movingHorizontally = false;
    }
}
