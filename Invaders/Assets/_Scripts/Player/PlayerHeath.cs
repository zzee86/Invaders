using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHeath : MonoBehaviour
{
    [SerializeField] float health, maxHealth;

    [SerializeField] private GameObject damagePopup;

    public HealthBarSystem healthBarSystem;

    bool isAlive = true;

    private SpawnPlayer spawnPlayer;
    [SerializeField] private ParticleSystem deathParticles;

public Vector2 testing;
    void Start()
    {
        health = maxHealth;
        healthBarSystem.SetHealth(health, maxHealth);

        spawnPlayer = GetComponentInParent<SpawnPlayer>();

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
            spawnPlayer.Spawn(new Vector2(-17f, -3.399f));
            isAlive = true;

        }

    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBarSystem.SetHealth(health, maxHealth);

        if (health <= 0)
        {
            Destroy(gameObject);
            isAlive = false;
            Instantiate(deathParticles, transform.position, Quaternion.identity);

        }

        GameObject points = Instantiate(damagePopup, transform.position, Quaternion.identity);
        points.transform.localPosition += new Vector3(0, 1.5f, 0);

        points.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());

        // Not needed because of animation
        // Destroy(points, 0.5f);
    }

    public void death()
    {
        Destroy(gameObject);
    }

}