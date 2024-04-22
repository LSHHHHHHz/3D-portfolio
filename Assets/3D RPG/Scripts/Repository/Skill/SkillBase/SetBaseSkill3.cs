using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetBaseSkill3 : SetBaseSkill
{
    List<GameObject> objList= new List<GameObject>();
    public GameObject effectPrefab;
    public override void Start()
    {
        base.Start();
    }
    public override void Execute(IActor actor, int damage)
    {
        transform.position = UnitManager.instance.player.transform.position + new Vector3(0, 2f, 0);
        this.subject = actor;
        this.damage = damage;
        StartCoroutine(SetSkill());
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ShootSKill3);
    }
    IEnumerator SetSkill()
    {
        int i = 0;
        while (i < 10)
        {
            GameObject obj = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<ProjectileSkill3>().actor = subject;
            obj.GetComponent<ProjectileSkill3>().damage = damage;
            if(i==0)
            {
                obj.GetComponent<MeshRenderer>().enabled= true;
            }
            else
            {
                obj.GetComponent<MeshRenderer>().enabled = false;
            }
            objList.Add(obj); 
            i++;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
