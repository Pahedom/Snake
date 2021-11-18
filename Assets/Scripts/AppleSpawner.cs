using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform applePrefab;

    [SerializeField] private Snake snake;

    [SerializeField] private Field field;

    [SerializeField] private int apples;

    private List<RectTransform> activeApples = new List<RectTransform>();

    public UnityAction OnEatApple;

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
        return new Vector2(Random.Range(0, field.size.x), Random.Range(0, field.size.y)) * field.tileSize;
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

        OnEatApple?.Invoke();
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
}