using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField] private Button LevelOneButton;

    [SerializeField] private Button LevelTwoButton;

    [SerializeField] private Button QuitButton;

    private bool canAccessLevelTwo = false;

    //Add listeners to all buttons on start 
    private void Start()
    {
        LevelOneButton.onClick.AddListener(OnLevelOneClick);

        LevelTwoButton.onClick.AddListener(OnLevelTwoClick);

        QuitButton.onClick.AddListener(OnQuitClick);
    }

    //On clicking level one button, load first scene
    private void OnLevelOneClick()
    {
        SceneManager.LoadScene(1);
    }

    //On clicking level two button, check if player has unlocked level two, if so then
    //load second level
    private void OnLevelTwoClick()
    {
        if (canAccessLevelTwo)
        {
            Debug.Log("Level Two Accessed!");
        }
    }

    //When clicking quit button
    private void OnQuitClick()
    {
        //Quit game
        Application.Quit();

        //If in Unity editor, exit application instead of quitting entire game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }


    //On destroy, remove all listeners in level 1,2 and quit button
    private void OnDestroy()
    {
        LevelOneButton.onClick.RemoveListener(OnLevelOneClick);

        LevelTwoButton.onClick.RemoveListener(OnLevelTwoClick);

        QuitButton.onClick.RemoveListener(OnQuitClick);
    }
}
