using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ProjectileSkill3 : ProjectileSkill
{
    public int speed;
    private void Start()
    {
        transform.position = UnitManager.instance.player.transform.position + new Vector3(0, 3, 0);
        StartCoroutine(MultipleGuidedMissiles());
    }
    IEnumerator MultipleGuidedMissiles()
    {
        yield return new WaitForSeconds(speed);
    }
}
