using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetBaseSkill3 : SetBaseSkill
{
    List<GameObject> objList= new List<GameObject>();
    GameObject effectPrefab;
    public float objSpeed =0.1f;
    public override void Start()
    {
        base.Start();
        transform.position = UnitManager.instance.player.transform.position + new Vector3(0, 3, 0);
        StartCoroutine(MultipleGuidedMissiles(10));
    }
    public override void Execute(int damage)
    {
    }
    IEnumerator MultipleGuidedMissiles(int count)
    {
        int num = 0;
        while (num < count)
        {
            num++;
            CheckObject();
            yield return new WaitForSeconds(0.1f);
        }
    }
    void CheckObject()
    {
        GameObject obj = null;
        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i].activeSelf)
            {
                obj = objList[i];
                break;
            }
        }
        if (obj == null)
        {
            GameObject objprefab = Instantiate(effectPrefab, transform);
            obj = objprefab;
            objList.Add(obj);
        }
    }
}
