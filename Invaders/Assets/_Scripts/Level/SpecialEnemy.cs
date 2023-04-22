using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;


    private float timeRemaining = 1.2f;
    [SerializeField] private float fireRate = 1.2f;


    void Start()
    {
        timeRemaining = 0;
    }
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

        }
        else
        {
            var copies = Instantiate(bullet, transform.position, Quaternion.identity);
            copies.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed;
            timeRemaining = fireRate;
            Destroy(copies, 15);
        }
    }
}
