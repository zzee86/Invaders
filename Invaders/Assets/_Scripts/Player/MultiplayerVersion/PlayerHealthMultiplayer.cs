using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHeathMultiplayer : MonoBehaviour
{
    [SerializeField] float health, maxHealth;

    [SerializeField] private GameObject damagePopup;

    public HealthBarSystem healthBarSystem;

    bool isAlive = true;

    [SerializeField] private ParticleSystem deathParticles;

    int trapLayer;



    void Start()
    {
        health = maxHealth;
        healthBarSystem.SetHealth(health, maxHealth);
        trapLayer = LayerMask.NameToLayer("Trap");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
        }

        if (isAlive == false)
        {
            Debug.Log("respawn");
            isAlive = true;
        }




    }

    public void TakeDamage(float damageAmount)
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

        // Not needed because of animation
        // Destroy(points, 0.5f);


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
        Instantiate(deathParticles, transform.position, Quaternion.identity);

        gameObject.SetActive(false);

        //Tell the Game Manager that the player died and tell the Audio Manager to play
        //the death audio
        GameManager.PlayerDied();
    }
}