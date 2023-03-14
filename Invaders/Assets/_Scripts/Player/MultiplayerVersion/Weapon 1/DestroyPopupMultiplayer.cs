using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPopupMultiplayer : MonoBehaviour
{
    void DestroyParent()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    }
}
