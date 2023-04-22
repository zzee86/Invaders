using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoomDoor : MonoBehaviour
{
    [SerializeField] private GameObject breakEffect;


    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Instant Death")
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
