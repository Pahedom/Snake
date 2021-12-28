using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform applePrefab;

    [SerializeField] private Snake snake;

    [SerializeField] private Field field;

    [SerializeField] private int apples;

    [SerializeField] private int scorePerApple;

    private List<RectTransform> activeApples = new List<RectTransform>();

    public UnityAction<int> OnEatApple;

    private void Awake()
    {
        SetApples(PlayerPrefs.GetInt("apples", 5).ToString());

        SetScorePerApple(PlayerPrefs.GetInt("scorePerApple", 1).ToString());
    }

    void Start()
    {
        snake.OnMove += TryEatApple;

        RectTransform newApple;
        for (int i = 0; i < apples; i++)
        {
            newApple = Instantiate(applePrefab, gameObject.transform);

            activeApples.Add(newApple);

            Reposition(newApple);
        }
    }

    void Reposition(RectTransform apple)
    {
        Vector2 newPosition;

        do
        {
            newPosition = RandomPosition();
        }
        while (FindApple(newPosition) || snake.CheckForBodyPiece(newPosition));

        apple.anchoredPosition = newPosition;
    }

    Vector2 RandomPosition()
    {
        return new Vector2(UnityEngine.Random.Range(0, field.size.x), UnityEngine.Random.Range(0, field.size.y)) * field.tileSize;
    }

    void TryEatApple(Vector2 position)
    {
        RectTransform apple = FindApple(position);

        if (!apple)
        {
            return;
        }

        snake.IncreaseLength();

        Reposition(apple);

        OnEatApple?.Invoke(scorePerApple);
    }

    RectTransform FindApple(Vector2 position)
    {
        foreach (RectTransform apple in activeApples)
        {
            if (apple.anchoredPosition == position)
            {
                return apple;
            }
        }

        return null;
    }

    public void SetApples(string newValue)
    {
        try
        {
            apples = Int32.Parse(newValue);
        }
        catch (FormatException)
        {
            return;
        }

        PlayerPrefs.SetInt("apples", apples);
    }

    public void SetScorePerApple(string newValue)
    {
        try
        {
            scorePerApple = Int32.Parse(newValue);
        }
        catch (FormatException)
        {
            return;
        }

        PlayerPrefs.SetInt("scorePerApple", scorePerApple);
    }
}