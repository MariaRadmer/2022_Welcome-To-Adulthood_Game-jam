using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager inputManager;
    private KeyCode interact = KeyCode.E;


    private void Awake() {
        if (inputManager == null) {
            DontDestroyOnLoad(gameObject);
            inputManager = this;
        } else if(inputManager != this){
            Destroy(gameObject);
        }
    }

    bool GetInteractPressed()
    {
        if (Input.GetKeyDown(interact)) {
            return true;
        } else {
            return false;
        }
    }

    public InputManager GetInstance() {
        return inputManager;
    }
}
