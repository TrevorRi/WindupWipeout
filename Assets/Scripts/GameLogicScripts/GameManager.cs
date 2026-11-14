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

    //Variable to store total elapsed time, regardless of time scaling
    private float totalElapsedTime = 0.0f;


    //Variable to store if timer is currently running
    private bool timerIsRunning;

    public static GameManager Manager { get; private set; }


    private void Awake()
    {
        //Creating singleton pattern
        if (Manager != null && Manager != this)
        {
            //Destroy other manager if another exists
            Destroy(gameObject);
            return;
        }

        //Setting only manager as this object
        Manager = this;

        defaultDeltaTime = Time.fixedDeltaTime;
        timeMovementTestScript = timeMovementTest.GetComponent<TestPlayerBlockMovement>();
    }


    private void Start()
    {
        //Start timer
        timerIsRunning = true;

        //Add listener for resume button being pressed
        //(done in start to allow for button to exist for a frame before being set as inactive)
        resumeButtonReference.onClick.AddListener(OnResumePressed);

        // Add listener for reset button being pressed
        resetButtonReference.onClick.AddListener(OnResetPressed);

        //Add listener for quit button being pressed
        quitButtonReferene.onClick.AddListener(OnQuitPressed);
    }


    private void FixedUpdate()
    {
        //If timer is running, add deltatime to value
        if (timerIsRunning)
        {
            totalElapsedTime += defaultDeltaTime;
            elapsedTime += Time.deltaTime;
        }
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
        //Reset time scale to basic value
        //(should be swapped for stored value once we have enemies/ skipping values being altered)
        Time.timeScale = 1.0f;

        //Change deltatime to reflect new value
        Time.fixedDeltaTime = defaultDeltaTime * Time.timeScale;
    }

    private void StopTime()
    {
        //Stop time scale completely
        Time.timeScale = 0.0f;

        //Change deltatime to reflect stopped value
        Time.fixedDeltaTime = defaultDeltaTime * Time.timeScale;
    }


    //Start of code for Scoring

    private int CalculateScore()
    {
        //Subtract values for total time and elapsed time, getting different in time it took for player to reach end
        //and expected value (varies by level)
        float totalTimeDifference = totalElapsedTime - elapsedTime;

        float finalScore = 100 - totalTimeDifference;

        return (int)finalScore;
    }


    //End of code for Scoring


    //Start of code for Menu UI

    [SerializeField] private GameObject canvasReference;

    [SerializeField] private Button resumeButtonReference;

    [SerializeField] private Button resetButtonReference;

    [SerializeField] private Button quitButtonReferene;

    //Bool for if menu is currently up
    private bool menuIsActive = true;


    //Public method for pulling up pause menu when escape is pressed
    public void OnEscapePressed(InputAction.CallbackContext ctx)
    {
        //If esc is pressed
        if (ctx.performed)
        {
            //Stop timer
            timerIsRunning = !timerIsRunning;

            //If menu is not active, time must be stopped (due to calling before value is changed), restart time again
            if (!menuIsActive)
            {
                StartTime();
            }
            //Otherwise, menu is active, stop time
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

    private void OnQuitPressed()
    {
        //Load scene of main menu (this will be changed before ending, since name will be outdated)
        SceneManager.LoadScene("MainMenuUITesting");
    }

    private void OnDestroy()
    {
        //Remove listeners from object upon being destroyed to prevent memory leak
        resumeButtonReference.onClick.RemoveListener(OnResumePressed);

        resetButtonReference.onClick.RemoveListener(OnResetPressed);

        quitButtonReferene.onClick.RemoveListener(OnQuitPressed);
    }

    //End of Code for Menu UI
}
