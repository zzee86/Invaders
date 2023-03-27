using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class DestroyableBlock : MonoBehaviour
{
    [SerializeField] private UnityEvent damage;


    void OnCollisionEnter2D(Collision2D collision2D)
    {
        var other = collision2D.collider.GetComponent<DamageBullet>();
        if (other && collision2D.contacts[0].normal.y > 0)
        {
            damage?.Invoke();
        }
    }
}
