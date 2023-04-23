using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int startingPoint;
    [SerializeField] private Transform[] points;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[index].position) <= 0.02f)
        {
            index++;
            if (index == points.Length)
            {
                index = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y < collision.transform.position.y - 0.5f)
        {
            collision.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
