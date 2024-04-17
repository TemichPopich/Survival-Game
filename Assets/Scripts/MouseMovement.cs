using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    
    public float mouseSencitivity = 100f;  

    private float xRotation = 0;
    private float yRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 

    }

}
