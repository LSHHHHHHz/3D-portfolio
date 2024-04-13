using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetBaseSkill2 : MonoBehaviour
{
    Vector3 targetPos;
    public float objSpeed =0.1f;
    private void Start()
    {
        targetPos = UnitManager.instance.player.playerTargettingEnemy.targetObj.transform.position;
        transform.position = UnitManager.instance.player.transform.position + new Vector3(0, 5, 0);
        StartCoroutine(EnergySkill());
    }
    IEnumerator EnergySkill()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(0.2f, 0.8f)).Append(transform.DOScale(0.1f, 0.8f));
        mySequence.SetLoops(5, LoopType.Yoyo);
        mySequence.Play();
        yield return mySequence.WaitForCompletion();
        yield return new WaitForSeconds(0.1f);
        mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(0.4f, 0.2f));
        mySequence.Play();
        yield return mySequence.WaitForCompletion();
        float elpased = 0;
        while (elpased<5)
        {
            Vector3 dir = (targetPos - transform.position).normalized;
            transform.position += dir * objSpeed * Time.deltaTime;
            elpased+= Time.deltaTime;
            yield return null;
        }
    }
}
