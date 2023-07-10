using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Note: attach to GameManager
    //Game objects that need to be dragged down in the Inspector
    [SerializeField] private bool isMainScene;   //only true in the scenes where the "Pause" scene should be loaded
    
    //Properties
    private bool isPaused { set; get; }

    private void Update()
    {
        //Pause/Unpause
        if (Input.GetKeyDown(KeyCode.Escape) && isMainScene)
        {
            PauseGame();
        }
    }

    //Methods
    //...Load scenes
    public void LoadMenu()
    {
        if (Time.timeScale != 1) Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void LoadGame()
    {
        if (Time.timeScale != 1) Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
    }

    public void LoadVictory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    //...Pause/Unpause
    public void PauseGame()
    {
        if (isPaused && isMainScene)
        {
            SceneManager.UnloadSceneAsync("Pause");
            Time.timeScale = 1f;
            isPaused = false;
        }
        else if (!isPaused && isMainScene)
        {
            if (SceneManager.GetSceneByName("Pause").isLoaded == false)
            {
                SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
                Time.timeScale = 0f;
                isPaused = true;
            }
        }
    }

    //...Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
