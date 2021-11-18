using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private Snake snake;

    [SerializeField] private AppleSpawner appleSpawner;

    [SerializeField] private Text scoreText;

    [SerializeField] private GameObject gameOverScreen;

    private int score = 1;

    void Start()
    {
        appleSpawner.OnEatApple += UpdateScore;

        scoreText.text = score.ToString();
    }

    void Update()
    {
        
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
}