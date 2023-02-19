using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private Transform gun;
    Vector2 direction;
        Vector2 direction2;


    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private GameObject bullet;


    [SerializeField]
    private float fireRate;


    private float readyForNextShot;

    int dirMultiplier;


private    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //MousePos - Relative to whole screen, Direction - Relative to Player
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
      //  direction = mousePos - (Vector2)gun.position;
       // direction2 = mousePos - (Vector2)shootPoint.position;

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
          //  FaceMouse();

transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

if(rotZ < 89 && rotZ > -89){
spriteRenderer.flipY = false;
spriteRenderer.flipX = false;

}
else {
spriteRenderer.flipY = true;
spriteRenderer.flipX = true;
}


//float horizontalInput = Input.GetAxis("Horizontal");



        if (Input.GetMouseButton(0))
        {
            if (Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + 1 / fireRate;
                ShootGun();
            }
        }
    }
    void FaceMouse()
    {
        if (transform.localScale == Vector3.one)
        {
//Debug.Log("right");
                        dirMultiplier = -1;
            transform.right = dirMultiplier * direction;
        }
        else
        {
      //      Debug.Log("left");

           dirMultiplier = 1;
            transform.right = dirMultiplier * direction;
}
    }

    void ShootGun()
    {
        GameObject bullets = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        bullets.GetComponent<Rigidbody2D>().AddForce(bullets.transform.right * bulletSpeed);
        Destroy(bullets, 2);
    }
}