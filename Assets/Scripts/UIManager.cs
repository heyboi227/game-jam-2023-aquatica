using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text highScoreText;
    [SerializeField]
    private TMP_Text gameOverText;
    [SerializeField]
    private TMP_Text restartLevelInstructionsText;
    [SerializeField]
    private Image lifeImage;
    [SerializeField]
    private Sprite[] lifeSprites;

    private GameManager gameManager;
    private SpawnManager spawnManager;

    private int score = 0;
    private int highScore = 0;

    private bool isPlayerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        gameOverText.enabled = false;
        restartLevelInstructionsText.enabled = false;
        if (!GameObject.Find("GameManager").TryGetComponent<GameManager>(out gameManager))
        {
            Debug.LogError("GameManager is null!");
        }
        if (!GameObject.Find("SpawnManager").TryGetComponent<SpawnManager>(out spawnManager))
        {
            Debug.LogError("SpawnManager is null!");
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void CheckHighScore()
    {
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High score: " + highScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
        lifeImage.sprite = lifeSprites[currentLives];
        if (currentLives <= 0)
        {
            isPlayerDead = true;
            }
        if (isPlayerDead)
        {
            DoGameOver();
        }
    }

    private void DoGameOver()
    {
        restartLevelInstructionsText.enabled = true;
        gameManager.GameOver();
        spawnManager.OnPlayerDeath();
        StartCoroutine(FlickerGameOver());
        CheckHighScore();
    }

    IEnumerator FlickerGameOver()
    {
        gameOverText.enabled = true;
        while (true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "You Lose";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
