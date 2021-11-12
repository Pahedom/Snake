using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Field field;

    [SerializeField] private RectTransform originalBody;

    [SerializeField] private float speed;

    private float timeBetweenMoves;

    private float timer;

    private List<RectTransform> bodyPieces = new List<RectTransform>();

    private Vector2 lastPosition;

    private Vector2 direction = Vector2.up;

    void Start()
    {
        timeBetweenMoves = 1f / speed;

        lastPosition = originalBody.anchoredPosition + Vector2.down * field.tileSize;

        bodyPieces.Add(originalBody.GetComponent<RectTransform>());
    }

    void Update()
    {
        GetDirection();

        timer += Time.deltaTime;

        if (timer >= timeBetweenMoves)
        {
            IncreaseLength();

            Move();

            timer -= timeBetweenMoves;
        }
    }

    void Move()
    {
        lastPosition = bodyPieces[bodyPieces.Count - 1].anchoredPosition;

        for (int i = bodyPieces.Count - 1; i > 0; i--)
        {
            bodyPieces[i].anchoredPosition = bodyPieces[i - 1].anchoredPosition;
        }

        Vector2 newPosition = originalBody.anchoredPosition + direction * field.tileSize;

        originalBody.anchoredPosition = newPosition;
    }

    void GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    public void IncreaseLength()
    {
        RectTransform newBodyPiece = Instantiate(originalBody, gameObject.transform);

        bodyPieces.Add(newBodyPiece);

        RectTransform lastBodyPiece = bodyPieces[bodyPieces.Count - 1];

        newBodyPiece.anchoredPosition = lastPosition;
    }
}
