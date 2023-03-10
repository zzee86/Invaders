using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DamageBullet : MonoBehaviourPunCallbacks
{
    [SerializeField] private float damageAmount;

    [SerializeField] private GameObject particles;

    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();

    }
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerController>()?.TakeDamage(30f);
    }


}
