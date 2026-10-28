using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject container;

    InputAction pauseAction;
    InputAction lookAction;

    void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Player/Pause");
        lookAction = InputSystem.actions.FindAction("Player/Look");
    }

    public void Update()
    {
        if (pauseAction.IsPressed())
        {
            container.SetActive(true);
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            lookAction.Disable();
        }
    }

    public void ResumeButton()
    {
        container.SetActive(false);
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lookAction.Enable();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
