using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTraps : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] private LayerMask playerLayer;
    private RaycastHit2D playerHit;
    [SerializeField] private float destroyTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }
    void DetectPlayer()
    {
        playerHit = Physics2D.Raycast(transform.position, Vector2.down, 20f, playerLayer);

        Debug.DrawRay(transform.position, Vector2.down * 20f, Color.red);
        if (playerHit)
        {
            body.gravityScale = 1f;
            Debug.Log("Falling");
            Destroy(gameObject, destroyTimer);
        }
    }
}