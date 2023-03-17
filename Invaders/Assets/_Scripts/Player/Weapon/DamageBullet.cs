using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBullet : MonoBehaviour
{
    [SerializeField] private float damageAmount = 45;

    [SerializeField] private GameObject particles;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damageAmount);

        }
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
