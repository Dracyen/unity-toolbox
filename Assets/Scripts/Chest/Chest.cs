using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour , Interactable
{
    private enum State { OPEN, CLOSED, ACTIVE, LOCKED }

    private enum Difficulty { EASY, MEDIUM, HARD }

    [SerializeField]
    private State currentState;

    private Animator animator;

    [SerializeField]
    private GameObject lockMesh;

    private void Awake()
    {
        Debug.Log("I'm Awake");

        animator = GetComponent<Animator>();

        if(currentState == State.LOCKED)
        {
            lockMesh.SetActive(true);
        }
        else
        {
            lockMesh.SetActive(false);
        }
    }

    public bool CanInteract()
    {
        if(currentState != State.ACTIVE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Interact()
    {
        if(currentState != State.ACTIVE)
        {
            switch (currentState)
            {
                case State.OPEN:

                    EnterState(State.ACTIVE);
                    animator.SetBool("Opened", false);

                    break;

                case State.CLOSED:

                    EnterState(State.ACTIVE);
                    animator.SetBool("Opened", true);

                    break;

                case State.LOCKED:

                    break;
            }
        }
    }

    private void EnterState(State state)
    {
        currentState = state;
    }
}