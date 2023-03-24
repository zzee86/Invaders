using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private bool isCollide;
    private PlatformEffector2D effector2D;

    void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (isCollide && Input.GetKeyDown(KeyCode.S))
        {
            effector2D.surfaceArc = 0f;
            Debug.Log("fall");
            StartCoroutine(WaitTime());
        }
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.3f);
        effector2D.surfaceArc = 125f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isCollide = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        isCollide = false;
    }
}
