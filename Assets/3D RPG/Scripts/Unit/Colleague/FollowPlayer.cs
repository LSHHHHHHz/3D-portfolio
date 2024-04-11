using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    Player player;
    public float distance;
    private void Update()
    {
        if (!DetectManager.instance.checkEnemyDetect)
        {
            DetectPlayerPos();
        }
    }

    void DetectPlayerPos()
    {
        player = UnitManager.instance.player;
        distance = Vector3.Distance(player.transform.position, transform.position);
    }

}
