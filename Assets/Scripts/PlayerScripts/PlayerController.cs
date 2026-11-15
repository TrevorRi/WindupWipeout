using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

enum playerStates
{
    alive,
    dive,
    Wind
}

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    public float maxWind;
    public float diveTime;
    public float windTime;
    private float dynamicDiveTime;
    private float dynamicWind;
    private float dynamicWindTime;
    private bool windBool;
    private Rigidbody2D rb;

    private playerStates playerState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dynamicWind = maxWind / 2; 
        dynamicDiveTime = diveTime;
        dynamicWindTime = windTime;
        windBool = true;
        rb = GetComponent<Rigidbody2D>();
        playerState = playerStates.alive;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerState == playerStates.alive)
        {
            //handle wind down time
            if(dynamicWind > 0)
            {
                dynamicWind -= Time.deltaTime;
            }

            if(dynamicWind < 0)
            {
                //stop at zero so movement speed does not turn negative
                dynamicWind = 0;
            }
            
            //move player rigid body using the wind amount as movement speed
            rb.linearVelocity = movement * dynamicWind;   
        }
        if(playerState == playerStates.dive)
        {
            if(dynamicDiveTime > 0)
            {
                dynamicDiveTime -= Time.deltaTime;
            }
            if(dynamicDiveTime <= .05)
            {
                rb.linearVelocity = movement * dynamicWind / 3;
            }
            if(dynamicDiveTime <= 0)
            {
                playerState = playerStates.alive;
                dynamicDiveTime = diveTime;
            }

            dynamicWind -= Time.deltaTime * 15;
            if(dynamicWind < 0)
            {
                //stop at zero so movement speed does not turn negative
                dynamicWind = 0;
            } 
        }
        if(playerState == playerStates.Wind)
        {
            if(dynamicWindTime > 0)
            {
                dynamicWindTime -= Time.deltaTime;
            }
            if(dynamicWindTime <= 0)
            {
                playerState = playerStates.alive;
            }
            
            //move player rigid body using the wind amount as movement speed
            rb.linearVelocity = movement * dynamicWind / 5; 
        }
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

            playerState = playerStates.Wind;
            dynamicWindTime = windTime;
        }
    }

    //press O and P at the same time to dive
    public void OnPlayerDive(InputAction.CallbackContext context)
    {
        if(context.performed && dynamicWind >= 1.5 )
        {
            playerState = playerStates.dive;
            rb.linearVelocity = movement * dynamicWind * 2;
            Debug.Log("Dive");
        }
    }

}
