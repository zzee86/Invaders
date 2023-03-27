using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteraction : MonoBehaviour
{
    [SerializeField] private BossDoor door;
    [SerializeField] private AgentController boss;
    [SerializeField] private Enemy bossHealth;
    void Start()
    {
        boss.bossStart += BossBattle_bossStart;
        bossHealth.bossEnd += BossBattle_bossEnd;
    }
    void BossBattle_bossStart(object sender, System.EventArgs e)
    {
        door.CloseDoor();
    }
    void BossBattle_bossEnd(object sender, System.EventArgs e)
    {
        door.OpenDoor();
    }
}
