using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

enum playerStatesNew
{
    alive,
    dive,
    wind,
    dead
}

public class PlayerControllerNew : MonoBehaviour
{
    private Vector2 movement;
    public float maxWind;
    public float diveTime;
    public float windTime;
    public List<Sprite> spriteList = new List<Sprite>();
    public List<RuntimeAnimatorController> animList = new List<RuntimeAnimatorController>();
    private float dynamicDiveTime;
    private float dynamicWind;
    private float dynamicWindTime;
    private bool windBool;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private playerStates playerState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dynamicWind = maxWind / 2; 
        dynamicDiveTime = diveTime;
        dynamicWindTime = windTime;
        windBool = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
            if(dynamicDiveTime <= .1)
            {
                rb.linearVelocity = movement * dynamicWind / 2;
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
        if(playerState == playerStates.wind)
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

        //animation controling updates
        if(movement.x < 0)
        {
            sprite.flipX = true;
        }
        else if(movement.x > 0)
        {
            sprite.flipX = false;
        }

        anim.speed = dynamicWind/(maxWind/2);

        if(movement*dynamicWind == Vector2.zero)
        {
            if(sprite.sprite != spriteList[0])
            {
                sprite.sprite = spriteList[0];
                anim.runtimeAnimatorController = animList[0];             
            }
        }
        else
        {
            if(sprite.sprite != spriteList[1])
            {
                sprite.sprite = spriteList[1];
                anim.runtimeAnimatorController = animList[1];
            }
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

            playerState = playerStates.wind;
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
        }
    }

    public void TakeDamage(Vector2 damageVector)
    {
        sprite.color = Color.red;
        sprite.color = Color.white;
    }
}
