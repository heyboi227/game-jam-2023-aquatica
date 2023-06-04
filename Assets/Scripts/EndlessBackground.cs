using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    public float speed;
    public float startX;
    public float endX;
    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * speed, startX - endX);
        transform.position = startPos + Vector2.left * newPos;
    }
}

