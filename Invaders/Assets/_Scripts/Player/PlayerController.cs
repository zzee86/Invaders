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
