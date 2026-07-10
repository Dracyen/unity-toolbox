using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleKeyScript : MonoBehaviour, Interactable
{
    [SerializeField]
    [Range(0,9)]
    private int number;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private KeypadScript keypad;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void Interact()
    {
        animator.SetTrigger("KeyIsDown");

        keypad.GiveNumber(number);
    }

    public void SetKeypad(KeypadScript keypad)
    {
        this.keypad = keypad;
    }
}