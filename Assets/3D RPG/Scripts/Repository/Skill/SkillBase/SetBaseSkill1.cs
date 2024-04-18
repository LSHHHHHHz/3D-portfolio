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
    public override void Execute(IActor actor, int damage)
    {
        this.subject= actor;
        StartCoroutine(HiperSpeedAtatck());
    }
    IEnumerator HiperSpeedAtatck()
    {
        if (player.playerTargettingEnemy.targetObj != null)
        {
            IActor actor = player.playerTargettingEnemy.targetObj.GetComponent<IActor>();
            SendDamageEvent sendDamage = new SendDamageEvent(subject, damage);
            actor.OnReceiveEvent(sendDamage);
        }
        player.GetComponent<TrailRenderer>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        camara.enabled = false;
        player.playerController.controller.enabled = false;
        Vector3 dir = (player.playerTargettingEnemy.targetObj.transform.position - player.gameObject.transform.position).normalized;
        Vector3 targetPos = player.playerTargettingEnemy.targetObj.transform.position + (dir * 1.2f);
        player.gameObject.transform.forward = dir;
        player.gameObject.transform.position = targetPos;
        camara.enabled = true;
        yield return new WaitForSeconds(0.1f);
        player.playerController.controller.enabled = true;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<TrailRenderer>().enabled = false;
    }
}
