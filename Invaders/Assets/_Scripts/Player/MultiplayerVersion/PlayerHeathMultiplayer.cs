using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
public class PlayerHeathMultiplayer : MonoBehaviourPunCallbacks
{
    [SerializeField] float health, maxHealth;

    [SerializeField] private GameObject damagePopup;

    public HealthBarSystem1 healthBarSystem;

    [SerializeField] private ParticleSystem deathParticles;

    int trapLayer;

    PhotonView pv;
    public PlayerManagerMultiplayer playerManagerMultiplayer;

    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; //Send event to all clients

    private const int WINNER = 1;
    private string winner;


    void Start()
    {
        health = maxHealth;
        healthBarSystem.SetHealth(health, maxHealth);
        trapLayer = LayerMask.NameToLayer("Trap");
        pv = GetComponent<PhotonView>();
        playerManagerMultiplayer = PhotonView.Find((int)pv.InstantiationData[0]).GetComponent<PlayerManagerMultiplayer>();
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
    }

    public void TakeDamage(float damageAmount)
    {

        // Not needed because of animation
        // Destroy(points, 0.5f);
        pv.RPC("RPC_TakeDamage", pv.Owner, damageAmount);
    }
    [PunRPC]
    private void RPC_TakeDamage(float damageAmount, PhotonMessageInfo info)
    {

        health -= damageAmount;
        healthBarSystem.SetHealth(health, maxHealth);

        if (health <= 0)
        {
            playerDeath();
            PlayerManagerMultiplayer.Find(info.Sender).GetKill();
            object[] winner = new object[] { PhotonNetwork.NickName, PhotonNetwork.PlayerListOthers[0].ToString() };
            PhotonNetwork.RaiseEvent(WINNER, winner, raiseEventOptions, SendOptions.SendReliable);

        }
        GameObject points = PhotonNetwork.Instantiate(damagePopup.name, transform.position, Quaternion.identity);
        points.transform.localPosition += new Vector3(0, 1.5f, 0);

        points.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == trapLayer)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
    }

    void playerDeath()
    {
        playerManagerMultiplayer.playerDeath();
    }
}