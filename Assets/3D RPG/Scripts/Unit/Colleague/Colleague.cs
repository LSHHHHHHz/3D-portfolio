using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Colleague : MonoBehaviour
{
    Player player;
    Vector3 spawnPos;
    private void Awake()
    {
        player = UnitManager.instance.player;
        if(player != null)
        {
            int distance = UnityEngine.Random.Range(0, 5);
            spawnPos.y = player.transform.position.y;
            spawnPos.z = player.transform.position.z + distance;
            spawnPos.x = player.transform.position.x + distance;
            transform.position = spawnPos;
        }
    }
}
