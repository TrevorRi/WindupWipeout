using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField] private Button LevelOneButton;

    [SerializeField] private Button LevelTwoButton;

    [SerializeField] private Button QuitButton;

    private bool canAccessLevelTwo = false;


    private void Start()
    {
        LevelOneButton.onClick.AddListener(OnLevelOneClick);

        LevelTwoButton.onClick.AddListener(OnLevelTwoClick);

        QuitButton.onClick.AddListener(OnQuitClick);
    }

    private void OnLevelOneClick()
    {
        SceneManager.LoadScene(1);
    }

    private void OnLevelTwoClick()
    {
        if (canAccessLevelTwo)
        {
            Debug.Log("Level Two Accessed!");
        }
    }

    private void OnQuitClick()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void OnDestroy()
    {
        LevelOneButton.onClick.RemoveListener(OnLevelOneClick);

        LevelTwoButton.onClick.RemoveListener(OnLevelTwoClick);

        QuitButton.onClick.RemoveListener(OnQuitClick);
    }
}
