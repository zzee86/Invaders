using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerHeathMultiplayer : MonoBehaviourPunCallbacks
{
    [SerializeField] float health, maxHealth;

    [SerializeField] private GameObject damagePopup;

    public HealthBarSystem1 healthBarSystem;

    bool isAlive = true;

    [SerializeField] private ParticleSystem deathParticles;

    int trapLayer;

    PhotonView pv;

    void Start()
    {
        health = maxHealth;
        healthBarSystem.SetHealth(health, maxHealth);
        trapLayer = LayerMask.NameToLayer("Trap");
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!pv.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
            Debug.Log("inflicted");
        }

        if (isAlive == false)
        {
            Debug.Log("respawn");
            isAlive = true;
        }




    }

    public void TakeDamage(float damageAmount)
    {

        // Not needed because of animation
        // Destroy(points, 0.5f);
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damageAmount);
    }
    [PunRPC]
    private void RPC_TakeDamage(float damageAmount)
    {
        if (pv.IsMine)
        {
            health -= damageAmount;
            healthBarSystem.SetHealth(health, maxHealth);

            if (health <= 0)
            {
                isAlive = false;

                playerDeath();

            }
            GameObject points = Instantiate(damagePopup, transform.position, Quaternion.identity);
            points.transform.localPosition += new Vector3(0, 1.5f, 0);

            points.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());

        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == trapLayer)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);

            playerDeath();
        }
    }

    void playerDeath()
    {
        pv.RPC("RPC_playerDeath", RpcTarget.All);
    }

    [PunRPC]
    void RPC_playerDeath()
    {
        PhotonNetwork.Instantiate(deathParticles.name, transform.position, Quaternion.identity);

        gameObject.SetActive(false);

        //Tell the Game Manager that the player died and tell the Audio Manager to play
        //the death audio
        GameManager.PlayerDied();
    }

}