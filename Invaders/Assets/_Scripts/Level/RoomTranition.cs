using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTranition : MonoBehaviour
{
    public GameObject virtualCam;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player") && !collider2D.isTrigger)
        {
            virtualCam.SetActive(true);
        }
    }
        void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player") && !collider2D.isTrigger)
        {
            virtualCam.SetActive(false);
        }
    }
}
