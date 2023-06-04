using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver = false;
    [SerializeField]
    private GameObject panel;
    private SpawnManager spawnManager;

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
        if (!GameObject.Find("SpawnManager").TryGetComponent(out spawnManager)) {
            Debug.LogError("Error! Spawn manager is null!");
        }
        spawnManager.StartSpawn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            StartNewGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P) && !isGameOver)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
