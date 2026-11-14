using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Start of Reference test variables

    [SerializeField] private GameObject timeMovementTest;

    private TestPlayerBlockMovement timeMovementTestScript;

    //End of Reference test varaibles



    //Start of Timer Variables

    //Variable to store current time in seconds
    private float elapsedTime = 0.0f;

    //Variable to store default value of delta time
    //(to scale when changing value of Time.timeScale, so objects actually slow down/ speed up)
    private float defaultDeltaTime;

    //Variable to store if timer is currently running
    private bool timerIsRunning;

    private void Awake()
    {
        defaultDeltaTime = Time.fixedDeltaTime;
        timeMovementTestScript = timeMovementTest.GetComponent<TestPlayerBlockMovement>();
    }

    //Subscribe/ unsubscribe from event
    private void OnEnable()
    {
        timeMovementTestScript.OnPlayerChangeSpeed += ShiftTime;
    }

    private void OnDisable()
    {
        timeMovementTestScript.OnPlayerChangeSpeed -= ShiftTime;
    }

    private void Start()
    {
        //Start timer
        timerIsRunning = true;

        //Add listener for resume button being pressed
        //(done in start to allow for button to exist for a frame before being set as inactive)
        resumeButtonReference.onClick.AddListener(OnResumePressed);

        //CTD- Add listener for quit button being pressed
        resetButtonReference.onClick.AddListener(OnResetPressed);

        //Setup 
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

    //Method to speed up or slow down game based on Unity events **Test Method**
    private void ShiftTime(bool isSlowdownOn)
    {

        if (isSlowdownOn)
        {
            //Set timescale to be new value
            Time.timeScale = 0.5f;

            //Set new deltatime to reflect new speed value
            Time.fixedDeltaTime = defaultDeltaTime * Time.timeScale;
        }
       
    }

    private void StartTime()
    {
        Time.timeScale = 1.0f;

        Time.fixedDeltaTime = defaultDeltaTime * Time.timeScale;
    }

    private void StopTime()
    {
        Time.timeScale = 0.0f;

        Time.fixedDeltaTime = defaultDeltaTime * Time.timeScale;
    }




    //Start of code for Menu UI

    [SerializeField] private GameObject canvasReference;

    [SerializeField] private Button resumeButtonReference;

    [SerializeField] private Button resetButtonReference;

    private bool menuIsActive = true;


    //Public method for pulling up pause menu when escape is pressed
    public void OnEscapePressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            //Stop timer
            timerIsRunning = !timerIsRunning;

            //If menu is active, time must be stopped, restart time again
            if (!menuIsActive)
            {
                StartTime();
            }
            //Otherwise, menu is not active, stop time
            else
            {
                StopTime();
            }

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

        //Start Game speed
        StartTime();

        //Set menu is active to false, 
        menuIsActive = !menuIsActive;

        //Unpause timer
        timerIsRunning = true;
        
    }

    private void OnResetPressed()
    {
        //Get name of current scene and load scene again
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void OnDestroy()
    {
        //Remove listener from object upon being destroyed to prevent memory leak
        resumeButtonReference.onClick.RemoveListener(OnResumePressed);

        resetButtonReference.onClick.RemoveListener(OnResetPressed);
    }

    //End of Code for Menu UI
}
