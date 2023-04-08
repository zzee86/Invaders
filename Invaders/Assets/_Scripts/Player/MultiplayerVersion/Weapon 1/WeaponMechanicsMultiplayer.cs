using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.IO;
using Photon.Realtime;
using UnityEngine.UI;

public class WeaponMechanicsMultiplayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject gunHolder;
    [SerializeField] private Transform gunPoint;
    [SerializeField] GameObject[] guns;

    private int gunIndex = 0;
    private int previousGunIndex = -1;

    PhotonView PV;

    // void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(gun[i]].transform.rotation);
    //     }
    //     else if (stream.IsReading)
    //     {
    //         Gun.transform.rotation = (Quaternion)stream.ReceiveNext();
    //     }
    // }


    // Start is called before the first frame update
    void Start()
    {
        guns[0].SetActive(true);

        for (int i = 1; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
        PV = GetComponent<PhotonView>();
    }



    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;

        //Switch to next/previous gun
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gunIndex == -1)
                gunIndex = 0;
            else if (gunIndex + 1 > guns.Length - 1)
                gunIndex = 0;
            else
                gunIndex += 1;

            EquipItem(gunIndex);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gunIndex == -1)
                gunIndex = 0;
            else if (gunIndex - 1 < 0)
                gunIndex = guns.Length - 1;
            else
                gunIndex -= 1;

            EquipItem(gunIndex);
        }
    }
    public void EquipItem(int index)
    {
        //If the player is trying to switch to the weapon already equipped return
        if (index == previousGunIndex)
            return;

        gunIndex = index;

        ShowItem(gunIndex, true);

        //If the previous item is showing, hide the gameObject
        if (previousGunIndex != -1)
        {
            ShowItem(previousGunIndex, false);
        }
        previousGunIndex = gunIndex;
    }


    public void ShowItem(int index, bool show)
    {
        PV.RPC("RPC_ShowItem", RpcTarget.All, index, show);
    }

    [PunRPC]
    void RPC_ShowItem(int index, bool show)
    {
        guns[index].SetActive(show);
    }

}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.IO;
using Photon.Realtime;
using UnityEngine.UI;
using Hashable = ExitGames.Client.Photon.Hashtable;
public class WeaponMechanics : MonoBehaviourPunCallbacks
{
    int totalWeapons = 1;
    [SerializeField]
    private int currentWeaponIndex;

    [SerializeField]
    private GameObject[] guns;
    [SerializeField]
    private GameObject weaponHolder;
    [SerializeField]
    private GameObject currentGun;

    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }
        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;
    }


    // Update is called once per frame

    void Update()
    {

    switchWeapon(currentWeaponIndex); // Show the prefab on this client



    }

    void switchWeapon(int index)
    {
        currentWeaponIndex = index;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentWeaponIndex < totalWeapons - 1)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];

            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentWeaponIndex > 0)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex -= 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];

            }
        }
        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("currentGun", currentGun);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            switchWeapon((int)changedProps["currentGun"]);
        }
    }
}
*/