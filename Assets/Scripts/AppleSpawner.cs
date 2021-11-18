using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform apple;

    [SerializeField] private Snake snake;

    [SerializeField] private Field field;

    [SerializeField] private int apples;

    private List<RectTransform> activeApples = new List<RectTransform>();

    void Start()
    {
        snake.OnMove += TryEatApple;

        for (int i = 0; i < apples; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        RectTransform newApple = Instantiate(apple, gameObject.transform);

        Vector2 newPosition;

        do
        {
            newPosition = RandomPosition();
        }
        while (FindApple(newPosition) != null || snake.CheckForBodyPiece(newPosition));

        newApple.anchoredPosition = newPosition;

        activeApples.Add(newApple);
    }

    Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(0, field.size.x), Random.Range(0, field.size.y)) * field.tileSize;
    }

    void TryEatApple(Vector2 position)
    {
        RectTransform apple = FindApple(position);

        if (apple == null)
        {
            return;
        }

        Destroy(apple.gameObject);

        activeApples.Remove(apple);

        snake.IncreaseLength();

        Spawn();
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