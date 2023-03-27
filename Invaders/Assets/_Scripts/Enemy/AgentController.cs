using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int index = 0;
    [SerializeField] private float distanceToStartHeadingToNextWaypoint;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float distanceToStartChasingTarget = 1f;
    float targetRange = 5f;
    [SerializeField] private Transform target;
    bool attackCooldown = true;
    [SerializeField] private float chaseSpeed = 8f;

    [SerializeField] private PlayerHeath playerHeath;

    private Animator anim;
    private State state;


    public event EventHandler bossStart;
    //Enemy enemy;

    private enum State
    {
        Patrolling,
        Chasing
    }
    void Start()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[0].position, moveSpeed * Time.deltaTime);
        state = State.Patrolling;
        anim = GetComponent<Animator>();
        // enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.Patrolling)
            Patrol();

        else
            Chase();
    }
    void Patrol()
    {
        FindTarget();
        anim.SetBool("isMoving", true);
        transform.position = Vector2.MoveTowards(transform.position, waypoints[index].position, moveSpeed * Time.deltaTime);

        if (waypoints[index] != null)
        {
            distanceToStartHeadingToNextWaypoint = Vector2.Distance(transform.position, waypoints[index].position);
            if (distanceToStartHeadingToNextWaypoint <= 1.5)
            {
                index = (index + 1) % waypoints.Length;

                transform.position = Vector2.MoveTowards(transform.position, waypoints[0].position, moveSpeed * Time.deltaTime);

                if (waypoints[index] == waypoints[0])
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (waypoints[index] == waypoints[1])
                {
                    transform.localScale = new Vector3(-1, 1, 1);

                }
            }
        }
    }
    void FindTarget()
    {
        distanceToStartChasingTarget = Vector2.Distance(target.position, transform.position);
        if (distanceToStartChasingTarget <= targetRange)
        {
            state = State.Chasing;
        }
    }

    void Chase()
    {
        // if (enemy.isBoss)
        // {
        bossStart?.Invoke(this, EventArgs.Empty);
        //}
        // transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // player on right side of enemy
        // test box is reversed
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
        }
        // player on left side of enemy
        else if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += Vector3.right * chaseSpeed * Time.deltaTime;

        }

        distanceToStartChasingTarget = Vector2.Distance(target.position, transform.position);

        if (distanceToStartChasingTarget > targetRange)
        {
            state = State.Patrolling;
        }

        if (distanceToStartChasingTarget <= 3)
        {
            if (attackCooldown)
            {
                StartCoroutine(AttackPlayer());
                attackCooldown = false;
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        anim.SetTrigger("attack");
        playerHeath.TakeDamage(20);
        yield return new WaitForSeconds(0.5f);
        attackCooldown = true;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            Debug.Log("player detectect");
        }
    }

}
