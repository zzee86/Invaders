using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class HealthBarSystem1 : MonoBehaviourPunCallbacks
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    Color color = new Color(233f / 255f, 79f / 255f, 55f / 255f);
    PhotonView pv;
    void Awake()
    {
        pv = GetComponent<PhotonView>();

    }
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);

    }
    public void SetHealth(float health, float maxHealth)
    {
        pv.RPC("RPC_SetHealth", RpcTarget.All, health, maxHealth);
    }
    [PunRPC]
    private void RPC_SetHealth(float health, float maxHealth)
    {
        Slider.gameObject.SetActive(health < maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, Slider.normalizedValue);

    }


}
