using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager current;
    Transform[] spawnPoints;

    void Awake()
    {
        current = this;
        spawnPoints = GetComponentsInChildren<Transform>();

    }
    public Transform getSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }
}
