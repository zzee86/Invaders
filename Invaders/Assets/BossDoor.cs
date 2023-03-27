using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // void OnTriggerEnter2D(Collider2D collider2D)
    // {
    //     if (collider2D.gameObject.tag == "Player")
    //     {
    //         CloseDoor();
    //     }
    // }

    public void CloseDoor()
    {
        anim.SetBool("close", true);
        Debug.Log("closed");
    }
    public void OpenDoor()
    {
        anim.SetBool("close", false);
        Debug.Log("opened");

    }
}
