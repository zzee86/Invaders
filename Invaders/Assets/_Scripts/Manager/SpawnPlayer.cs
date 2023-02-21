using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 spawnPoint;

void Start(){
Instantiate(player, spawnPoint, Quaternion.identity);
}

}
