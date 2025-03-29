using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPaused { get; private set; } = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        Debug.Log("Juego Iniciado");
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        Debug.Log("¡Has perdido!");
    }

    public void GameWon()
    {
        Debug.Log("¡Has ganado!");
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        Debug.Log(IsPaused ? "Pausa" : "Despausa");
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void ReturnToMainMenu()
    {
        if (IsPaused)
        {
            TogglePause();
        }
        Debug.Log("Regresando al menú principal...");
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public bool HasNextLevel()
    {
        return SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings;
    }
}
