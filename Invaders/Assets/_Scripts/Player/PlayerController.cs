using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviourPunCallbacks
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private Animator anim;
    private bool grounded = true;
    [SerializeField] int maxJumps;
    private int jumpCount;
    [SerializeField] private float jumpPower;
    bool facingRight = true;
    [SerializeField] private ParticleSystem jumpDust;


    [Header("Wall Slide")]
    private bool isWallSliding;
    private float wallSlideSpeed = 4;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;


    [Header("Wall Jump")]
    private bool isWallJump;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);


    int groundLayer;
    [SerializeField] private ParticleSystem deathParticles;

    GameManager gameManager;


    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); //Gets component from game object from inspector tab
        anim = GetComponent<Animator>();
        jumpCount = maxJumps;
        Physics2D.IgnoreLayerCollision(3, 7);

        groundLayer = LayerMask.NameToLayer("Ground");
    }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        transform.position = gameManager.lastCheckPoint;
    }
    private void Update()
    {


        if (GameManager.IsGameOver())
            return;

        float horizontalInput = Input.GetAxis("Horizontal"); //Store horizontal Input (-1, 0 ,1)

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //Flip player when changing direction
        if (horizontalInput > 0.01f && !facingRight)
        {
            flip();
        }
        else if (horizontalInput < -0.01f && facingRight)
        {
            flip();
        }

        //Jump - GetKeyDown used to only register the initial click, not holding the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount > 0)
            {
                Jump();

            }
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);

        wallSlide();
        wallJump();
    }
    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        generateDust();
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        anim.SetTrigger("Jump");

        if (grounded)
        {
            generateDust();
        }
        jumpCount -= 1;
        grounded = false;
    }

    public bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void wallSlide()
    {
        if (isWalled() && !grounded)
        {
            isWallSliding = true;
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlideSpeed, float.MaxValue));

            //  shootGun.enabled = false;
        }
        else
        {
            isWallSliding = false;
        }

    }
    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJump = false;
            wallJumpingDirection = -transform.rotation.x;

            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));

        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJump = true;
            body.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            generateDust();

            if (transform.localScale.x != wallJumpingDirection)
            {
                flip();
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJump = false;
    }

    private void generateDust()
    {
        jumpDust.Play();
    }
    //When player collides with Ground, reset number of jumps
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            grounded = true;
        }
        jumpCount = maxJumps;
    }
}
