using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth;

    [SerializeField] private GameObject damagePopup;

    public HealthBarSystem healthBarSystem;
    //  [SerializeField] Slider slider;
    // public Color low;
    // public Color High;
    // public Vector3 offset;

    void Start()
    {
        health = maxHealth;
        healthBarSystem.SetHealth(health, maxHealth);

        //   slider.maxValue = maxHealth;
        //   slider.value = health;
        //   slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, High, slider.normalizedValue);

    }
    // void Update(){

    //     slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

    // }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBarSystem.SetHealth(health, maxHealth);
        //  slider.gameObject.SetActive(health < maxHealth);
        //slider.value = health;


        if (health <= 0)
        {
            Destroy(gameObject);
        }
        GameObject points = Instantiate(damagePopup, transform.position, Quaternion.identity);
        points.transform.localPosition += new Vector3(0, 1.5f, 0);

        points.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());

        // Not needed because of animation
        //Destroy(points, 0.5f);
    }
}
