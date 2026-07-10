using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager inputManager;

    public static InputManager Instance
    {
        get
        {
            if (!inputManager)
            {
                inputManager = FindObjectOfType(typeof(InputManager)) as InputManager;

                if (!inputManager)
                {
                    Debug.LogError("There needs to be one active InputManager script on a GameObject in your scene.");
                }
            }

            return inputManager;
        }
    }

    [SerializeField]
    private Dictionary<string, InputManager> profiles;

    private InputProfile currentInputProfile;

    public void RequestProfile()
    {

    }

    private void SwitchProfiles()
    {

    }
}
