using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Snake : MonoBehaviour
{
    [SerializeField] private Field field;

    [SerializeField] private RectTransform head;

    [Min(0)]
    [SerializeField] private float speed;

    private float timeBetweenMoves;

    private int length = 1;

    private Vector2 direction;

    private Vector2 newDirection;

    [System.NonSerialized] public List<RectTransform> bodyPieces = new List<RectTransform>();

    private Vector2 lastPosition;

    private float timer;

    public UnityAction<Vector2> OnMove;

    public UnityAction OnDie;

    private void Awake()
    {
        SetSpeed(PlayerPrefs.GetFloat("speed", 8f).ToString());
    }

    void Start()
    {
        timeBetweenMoves = 1f / speed;

        bodyPieces.Add(head);

        length = 1;

        direction = Vector2.up;

        newDirection = Vector2.up;

        lastPosition = head.anchoredPosition + Vector2.down * field.tileSize;
    }

    void Update()
    {
        GetDirection();

        timer += Time.deltaTime;

        if (timer >= timeBetweenMoves)
        {
            Move();

            timer -= timeBetweenMoves;
        }
    }

    void Move()
    {
        direction = UpdatedDirection();

        lastPosition = bodyPieces[length - 1].anchoredPosition;

        for (int i = length - 1; i > 0; i--)
        {
            bodyPieces[i].anchoredPosition = bodyPieces[i - 1].anchoredPosition;
        }

        Vector2 newPosition = head.anchoredPosition + direction * field.tileSize;

        if (CheckForBodyPiece(newPosition) || CheckForEdge(newPosition))
        {
            OnDie?.Invoke();

            return;
        }

        head.anchoredPosition = newPosition;

        OnMove?.Invoke(newPosition);
    }

    void GetDirection()
    {
        Vector2 axis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if ((axis.x == 0f || axis.y == 0f) && axis != Vector2.zero)
        {
            newDirection = axis;
        }
    }

    Vector2 UpdatedDirection()
    {
        if (direction == Vector2.up && newDirection == Vector2.down)
        {
            return direction;
        }
        else if (direction == Vector2.left && newDirection == Vector2.right)
        {
            return direction;
        }
        else if (direction == Vector2.down && newDirection == Vector2.up)
        {
            return direction;
        }
        else if (direction == Vector2.right && newDirection == Vector2.left)
        {
            return direction;
        }

        return newDirection;
    }

    public void IncreaseLength()
    {
        RectTransform newBodyPiece = Instantiate(head, gameObject.transform);

        bodyPieces.Add(newBodyPiece);

        length++;

        newBodyPiece.anchoredPosition = lastPosition;

        newBodyPiece.name = "Snake Body " + (length - 1);
    }

    public bool CheckForBodyPiece(Vector2 position)
    {
        foreach (RectTransform bodyPiece in bodyPieces)
        {
            if (position == bodyPiece.anchoredPosition)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckForEdge(Vector2 position)
    {
        if (position.x < 0 || position.x >= field.size.x * field.tileSize)
        {
            return true;
        }

        if (position.y < 0 || position.y >= field.size.y * field.tileSize)
        {
            return true;
        }

        return false;
    }

    public void SetSpeed(string newValue)
    {
        try
        {
            speed = float.Parse(newValue);
        }
        catch (FormatException)
        {
            return;
        }

        PlayerPrefs.SetFloat("speed", speed);
    }
}