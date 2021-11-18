using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    [SerializeField] private RectTransform head;

    [Min(0)]
    [SerializeField] private float speed;

    private float timeBetweenMoves;

    private int length = 1;

    private Vector2 direction;

    [HideInInspector] public List<RectTransform> bodyPieces = new List<RectTransform>();

    private Vector2 lastPosition;

    private float timer;

    private Field field;

    public UnityAction<Vector2> OnMove;

    public UnityAction OnDie;

    private void OnValidate()
    {
        head = GetComponentsInChildren<RectTransform>()[1];

        timeBetweenMoves = 1f / speed;

        field = FindObjectOfType<Field>();

        lastPosition = head.anchoredPosition + Vector2.down * field.tileSize;
    }

    void Start()
    {
        bodyPieces.Add(head);

        length = 1;

        direction = Vector2.up;
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
        lastPosition = bodyPieces[length - 1].anchoredPosition;

        for (int i = length - 1; i > 0; i--)
        {
            bodyPieces[i].anchoredPosition = bodyPieces[i - 1].anchoredPosition;
        }

        Vector2 newPosition = head.anchoredPosition + direction * field.tileSize;

        if (CheckForBodyPiece(newPosition))
        {
            OnDie?.Invoke();

            return;
        }

        head.anchoredPosition = newPosition;

        OnMove?.Invoke(newPosition);
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
}