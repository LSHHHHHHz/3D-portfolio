using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetBaseSkill1 : SetBaseSkill
{
    CameraFollow camara;
    public override void Start()
    {
        base.Start();
        camara = player.cameraFollow.GetComponent<CameraFollow>();
    }
    public override void Execute(int damage)
    {
        StartCoroutine(HiperSpeedAtatck());
    }
    IEnumerator HiperSpeedAtatck()
    {
        player.GetComponent<TrailRenderer>().enabled = true;
        camara.enabled = false;
        player.playerController.controller.enabled = false;
        player.playerStatus.ConsumMP(MPConsum);
        Vector3 dir = (player.playerTargettingEnemy.targetObj.transform.position - player.gameObject.transform.position).normalized;
        Vector3 targetPos = player.playerTargettingEnemy.targetObj.transform.position + (dir * 1.2f);
        player.gameObject.transform.forward = dir;
        player.gameObject.transform.position = targetPos;
        camara.enabled = true;
        yield return new WaitForSeconds(0.1f);
        player.playerController.controller.enabled = true;
        yield return new WaitForSeconds(3f);
        player.GetComponent<TrailRenderer>().enabled = false;
    }
}
