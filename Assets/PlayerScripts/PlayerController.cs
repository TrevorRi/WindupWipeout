using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    public float maxWind;
    private float dynamicWind;
    private bool windBool;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dynamicWind = maxWind / 2; 
        windBool = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //handle wind down time
        if(dynamicWind > 0)
        {
            dynamicWind -= Time.deltaTime * 2;
        }

        if(dynamicWind < 0)
        {
            //stop at zero so movement speed does not turn negative
            dynamicWind = 0;
        }
        
        //move player rigid body using the wind amount as movement speed
        rb.linearVelocity = movement * dynamicWind;
    }

    public void OnPlayerMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnPlayerWind(InputAction.CallbackContext context)
    {
        string controlName = context.action.name;
        
        //alternate O and P key presses to wind the player
        if(context.performed)
        {
            if(controlName == "Wind1" && windBool == true)
            {
                dynamicWind += .5f;
                windBool = false;          
            }
            else if(controlName == "Wind2" && windBool == false)
            {
                dynamicWind += .5f;
                windBool = true;  
            }
            
            if(dynamicWind > maxWind)
            {
                dynamicWind = maxWind;
            }
        }
    }

    //press O and P at the same time to dive
    public void OnPlayerDive(InputAction.CallbackContext context)
    {
        // if(context.performed)
        // {
        //     Debug.Log("Dive");
        // }
    }
}
