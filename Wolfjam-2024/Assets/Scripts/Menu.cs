using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject resumeButton;

    public EventSystem eventSystem;

    private Controls controls;

    void Start()
    {

    }

    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        controls.UI.Pause.performed += ctx => Pause();
        controls.UI.Pause.Enable();
    }

    void OnDisable()
    {
        controls.UI.Pause.Disable();
    }

    void Update()
    {

    }

    public void Pause()
    {
        if(!PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(true);

            eventSystem.SetSelectedGameObject(resumeButton);
        }
        else
        {
            Resume();
        }
    }

    public void Play()
    {
        // Load First Level
        SceneManager.LoadScene("Level1");
    }

    public void Credits()
    {
        // Load Credits Scene
        SceneManager.LoadScene("Credits");
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        // Load Main Menu Scene
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
