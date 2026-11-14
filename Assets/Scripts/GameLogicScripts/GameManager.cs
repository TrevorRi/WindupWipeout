using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Start of Timer Variables

    //Variable to store current time in seconds
    private float elapsedTime = 0.0f;

    //Variable to store if timer is currently running
    private bool timerIsRunning;


    private void Start()
    {
        //Start timer
        timerIsRunning = true;

        //Add listener for resume button being pressed 
        resumeButtonReference.onClick.AddListener(OnResumePressed);

        //CTD- Add listener for quit button being pressed
    }


    private void FixedUpdate()
    {
        //If timer is running, add deltatime to value
        if (timerIsRunning)
        {
            elapsedTime += Time.deltaTime;
        }

        //Debug to log value of time for testing pause
        Debug.Log(elapsedTime);
    }


    //Start of code for Menu UI

    [SerializeField] private GameObject canvasReference;

    [SerializeField] private Button resumeButtonReference;

    private bool menuIsActive = true;


    //Public method for pulling up pause menu when escape is pressed
    public void OnEscapePressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            //Stop timer
            timerIsRunning = !timerIsRunning;

            //Set canvas for pause menu to be active 
            canvasReference.SetActive(menuIsActive);

            //Change status of menuIsActive to reflect value
            menuIsActive = !menuIsActive;
        }  
    }

    //Copy method for handling resume button pressed 
    private void OnResumePressed()
    {
        //Set canvas for pause menu to be inactive 
        canvasReference.SetActive(menuIsActive);

        //Set menu is active to false, 
        menuIsActive = !menuIsActive;

        //Unpause timer
        timerIsRunning = true;
        
    }

    private void OnDestroy()
    {
        //Remove listener from object upon being destroyed to prevent memory leak
        resumeButtonReference.onClick.RemoveListener(OnResumePressed);
    }

    //End of Code for Menu UI
}
