using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadScript : MonoBehaviour
{
    private bool canInput = true;

    [SerializeField]
    private string correctCode;

    [SerializeField]
    private string currentCode;

    [SerializeField]
    private SingleKeyScript[] keys;

    [SerializeField]
    private TextMesh display;

    private void Start()
    {
        foreach(SingleKeyScript key in keys)
        {
            key.SetKeypad(this);
        }
    }

    public void GiveNumber(int i)
    {
        if(canInput)
        {
            InputNumber(i);
        }
    }

    private void InputNumber(int i)
    {
        currentCode += i.ToString();

        display.text = currentCode;

        if (currentCode.Length == correctCode.Length)
        {
            canInput = false;

            CheckCode();
        }
    }

    private void CheckCode()
    {
        if(currentCode == correctCode)
        {
            StartCoroutine(ResetKeypad("✓"));
        }
        else
        {
            StartCoroutine(ResetKeypad("✗"));
        }
    }

    IEnumerator ResetKeypad(string i)
    {
        yield return new WaitForSeconds(1);

        display.text = i;

        yield return new WaitForSeconds(2);

        currentCode = "";

        display.text = "";

        canInput = true;
    }
}
