using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DamageBulletMultiplayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private float damageAmount;

    [SerializeField] private GameObject particles;

    PhotonView PV;

    // Method not being called in multiplayer

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log("bullet");
    }
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("photon bullet");
        // collision.gameObject.GetComponent<PlayerHeathMultiplayer>()?.TakeDamage(30f);
        if (collision.gameObject.TryGetComponent<PlayerHeathMultiplayer>(out PlayerHeathMultiplayer enemyComponent))
        {
            enemyComponent.TakeDamage(damageAmount);

        }
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
