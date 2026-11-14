using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuBehavior : MonoBehaviour
{

    [SerializeField] Button resumeButton;

    [SerializeField] Button quitButton;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    


}
