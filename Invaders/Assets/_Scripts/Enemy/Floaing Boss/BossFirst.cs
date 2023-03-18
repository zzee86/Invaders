using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFirst : MonoBehaviour
{
    public float idleMoveSpeed;
    public Vector2 idleMoveDirection;


    public float attackMoveSpeed;
    public Vector2 attackMoveDirection;


    public float attackPlayerSpeed;
    public Transform player;
    public Vector2 playerPos;


    public Transform groundUp;
    public Transform groundDown;
    public Transform groundWall;
    public float groundRadius;

    public LayerMask groundLayer;
    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private Rigidbody2D body;

    bool goingUp = true;
    bool facingLeft = true;


    bool attackCooldown = true;

    [SerializeField] private PlayerHeath playerHeath;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundUp.position, groundRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundDown.position, groundRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundWall.position, groundRadius, groundLayer);

        AttackState();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackPlayer();
        }
        // FlipTowardsPlayer();
    }
    public void IdleState()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }

        body.velocity = idleMoveSpeed * idleMoveDirection;
    }
    public void AttackState()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }

        body.velocity = Random.Range(20, 25) * attackMoveDirection;
    }
    public void AttackPlayer()
    {
        Debug.Log("ran");
        playerPos = player.position - transform.position;
        playerPos.Normalize();
        body.velocity = playerPos * attackPlayerSpeed;
    }
    void FlipTowardsPlayer()
    {
        float playerDir = player.position.x - transform.position.x;
        if (playerDir > 0 && facingLeft)
        {
            Flip();
        }
        else if (playerDir < 0 && !facingLeft)
        {
            Flip();
        }
    }
    void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;

    }
    void Flip()
    {
        facingLeft = !facingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundUp.position, groundRadius);
        Gizmos.DrawWireSphere(groundDown.position, groundRadius);
        Gizmos.DrawWireSphere(groundWall.position, groundRadius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHeath.TakeDamage(50);
        }
    }
}

