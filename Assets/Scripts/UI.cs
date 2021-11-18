using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private Snake snake;

    [SerializeField] private AppleSpawner appleSpawner;

    [SerializeField] private Text scoreText;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Text finalScoreText;

    private int score = 1;

    void Start()
    {
        appleSpawner.OnEatApple += UpdateScore;

        snake.OnDie += GameOver;

        scoreText.text = score.ToString();

        Time.timeScale = 1;

        gameOverScreen.SetActive(false);
    }

    void UpdateScore()
    {
        score++;

        scoreText.text = score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        Time.timeScale = 0;

        gameOverScreen.SetActive(true);

        finalScoreText.text = score.ToString();
    }
}