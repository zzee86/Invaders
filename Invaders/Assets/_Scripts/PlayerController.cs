using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float health, maxHealth;

    [SerializeField] private GameObject damagePopup;

    public HealthBarSystem healthBarSystem;

    bool isAlive = true;

    private SpawnPlayer spawnPlayer;
    //[SerializeField] private Vector2 spawnpoint;


    void Start()
    {
        health = maxHealth;
        healthBarSystem.SetHealth(health, maxHealth);
        //   slider.maxValue = maxHealth;
        //   slider.value = health;
        //   slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, High, slider.normalizedValue);

        spawnPlayer = GetComponentInParent<SpawnPlayer>();

    }
    // void Update(){

    //     slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
        }

        if (isAlive == false)
        {
            // Debug.Log("Restart the Game");
            // Vector2 spawnPoint = gameObject.transform.position - new Vector3(3f, 0f, 0f);
            Debug.Log("respawn");
            spawnPlayer.Spawn(new Vector2(-17f, -3.399f));
            isAlive = true;

        }

    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBarSystem.SetHealth(health, maxHealth);
        //  slider.gameObject.SetActive(health < maxHealth);
        //slider.value = health;


        if (health <= 0)
        {
            Destroy(gameObject);
            isAlive = false;
        }

        GameObject points = Instantiate(damagePopup, transform.position, Quaternion.identity);
        points.transform.localPosition += new Vector3(0, 1.5f, 0);

        points.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());

        // Not needed because of animation
        // Destroy(points, 0.5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Natural Damage")
        {
            TakeDamage(5);
        }
        else if (collision.gameObject.tag == "Instant Death")
        {

            death();
            //   TakeDamage(50);
        }

    }

    private IEnumerator Delayer()
    {
        yield return new WaitForSeconds(1.2f);
        isAlive = false;
        Destroy(gameObject, 1.2f);

    }
    void miniJump()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        Vector2 other = gameObject.transform.position + new Vector3(0f, 2f, 0f);
        transform.position = other;
    }
    void death()
    {
        miniJump();
        gameObject.GetComponent<Movement>().enabled = false;
        StartCoroutine(Delayer());

    }

}