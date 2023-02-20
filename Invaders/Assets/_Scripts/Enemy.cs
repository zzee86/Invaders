using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] private GameObject damagePopup;

    private float disappearTimer;

    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        GameObject points = Instantiate(damagePopup, transform.position, Quaternion.identity);
        points.transform.localPosition += new Vector3(0, 1, 0);

        points.GetComponent<TextMeshPro>().text = "" + damageAmount;

        Destroy(points, 0.5f);
    }
}
