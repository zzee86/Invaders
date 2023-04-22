using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapearBlock : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Instant Death")
        {
            StartCoroutine(fadingBlock());
        }
    }
    IEnumerator fadingBlock()
    {
        animator.SetBool("fade", true);
        yield return new WaitForSeconds(2);

        animator.SetBool("fade", false);
        yield return new WaitForSeconds(1);

    }
}
