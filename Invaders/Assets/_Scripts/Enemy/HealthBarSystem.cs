using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    Color color = new Color(233f / 255f, 79f / 255f, 55f / 255f);

    void Update()
    {

        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
    public void SetHealth(float health, float maxHealth)
    {
        Slider.gameObject.SetActive(health < maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, Slider.normalizedValue);

    }
}
