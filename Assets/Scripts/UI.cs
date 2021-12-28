using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private Snake snake;

    [SerializeField] private AppleSpawner appleSpawner;

    [SerializeField] private Text scoreText;

    [SerializeField] private Text bestScoreText;

    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private InputField speedIF;

    [SerializeField] private InputField applesIF;

    [SerializeField] private InputField scorePerAppleIF;

    private int score;

    void Start()
    {
        appleSpawner.OnEatApple += UpdateScore;

        snake.OnDie += GameOver;

        if (PlayerPrefs.GetInt("Restart", 0) == 0)
        {
            Time.timeScale = 0;

            mainMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;

            mainMenu.SetActive(false);
        }

        PlayerPrefs.SetInt("Restart", 0);

        gameOverScreen.SetActive(false);

        DisplayScore();

        speedIF.text = PlayerPrefs.GetFloat("speed").ToString();

        applesIF.text = PlayerPrefs.GetInt("apples").ToString();

        scorePerAppleIF.text = PlayerPrefs.GetInt("scorePerApple").ToString();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }

        if (pauseScreen.activeSelf)
        {
            Pause(false);
        }
        else if (Time.timeScale == 1)
        {
            Pause(true);
        }
    }

    void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > PlayerPrefs.GetInt("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", score);
        }

        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = score.ToString();

        bestScoreText.text = PlayerPrefs.GetInt("bestScore").ToString();
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("Restart", 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        Time.timeScale = 0;

        gameOverScreen.SetActive(true);
    }

    public void Pause(bool pause)
    {
        pauseScreen.SetActive(pause);

        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}