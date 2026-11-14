using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TestPlayerBlockMovement : MonoBehaviour
{
    Vector2 directionLeft = new Vector2(-1, 0);

    [SerializeField] private float maxSpeed = 1.0f;

    private Vector2 velocity;

    //Creating event for player changing the game speed 
    //(bool for testing)
    public event Action<bool> OnPlayerChangeSpeed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * maxSpeed * Time.deltaTime);

        
    }


    //Method to have player slow game upon key press (done to .5 speed)
    public void OnPlayerSlowGame(InputAction.CallbackContext ctx)
    {
        //If button pressed, invoke event
        if (ctx.performed)
        {
            OnPlayerChangeSpeed?.Invoke(true);
        }
    }
}
