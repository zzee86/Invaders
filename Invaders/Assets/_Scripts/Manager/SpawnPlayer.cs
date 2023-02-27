using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 spawnPoint;


    //[SerializeField] private GameObject eObject;

    void Start()
    {
        Spawn(spawnPoint);
    }


    public void Spawn(Vector2 spawnPoint)
    {
        this.spawnPoint = spawnPoint;
        GameObject temp = Instantiate(player, spawnPoint, Quaternion.identity);
        temp.transform.parent = gameObject.transform;
        
    }
}
