using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DamageBullet : MonoBehaviourPunCallbacks
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
        Debug.Log("oncollision");
    //    collision.gameObject.GetComponent<PlayerControllerMultiplayer>()?.TakeDamage(30f);
    }


}
