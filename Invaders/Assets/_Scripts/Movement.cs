using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D body;
    [SerializeField] private float speed;
    private Animator anim;
    private bool grounded = true;
    [SerializeField] int maxJumps;
    private int jumpCount;

    [SerializeField] private float jumpPower;

    bool facingRight = true;

    // Start is called before the first frame update

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); //Gets component from game object from inspector tab
        anim = GetComponent<Animator>();
        jumpCount = maxJumps;
        Physics2D.IgnoreLayerCollision(3, 7);

    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //Store horizontal Input (-1, 0 ,1)

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        //Flip player when changing direction
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Jump - GetKeyDown used to only register the initial click, not holding the space bar
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            if (jumpCount > 0)
            {
                Jump();
            }
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);




Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

if(mousePos.x <transform.position.x && facingRight){
    flip();
} else if(mousePos.x > transform.position.x && !facingRight){
    flip();
}

    }
    void flip(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f,0f);
    }


    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("Jump");
        jumpCount -= 1;
        grounded = false;
    }

    //When player collides with Ground, reset number of jumps
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        jumpCount = maxJumps;
    }
}
