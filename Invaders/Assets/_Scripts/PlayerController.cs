using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Slider slider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            TakeDamage(200);
        }
    }

    void TakeDamage(float damage)
    {
        Debug.Log("take damage for slider");
        slider.value = 0.4f;
    }
}