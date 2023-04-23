using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHeath>(out PlayerHeath playerComponent))
        {
            playerComponent.TakeDamage(30);
Debug.Log("hittt");
        }
        Destroy(gameObject);
    }
}
