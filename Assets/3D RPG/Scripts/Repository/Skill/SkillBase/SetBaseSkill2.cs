using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetBaseSkill2 : SetBaseSkill
{    
    public Vector3 targetPos;
    public float objSpeed = 2f;
    public override void Start()
    {
        base.Start();
    }
    public override void Execute(IActor actor, int damage)
    {
        this.subject = actor;
        gameObject.SetActive(true);
        StartCoroutine(EnergySkill());
    }
    IEnumerator EnergySkill()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        player = UnitManager.instance.player;
        if (UnitManager.instance.player.playerTargettingEnemy.targetObj != null)
        {
            targetPos = UnitManager.instance.player.playerTargettingEnemy.targetObj.transform.position;

            transform.position = UnitManager.instance.player.transform.position + new Vector3(0, 5, 0);
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
            while (elpased < 5)
            {
                Vector3 dir = (targetPos - transform.position).normalized;
                transform.position += dir * objSpeed * Time.deltaTime;
                elpased += Time.deltaTime;
                yield return null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection"))
        {
            IActor actor = other.GetComponent<IActor>();
            if (actor != null)
            {
                if (actor is CharacterStatusBase actorstatus)
                {
                    if (actorstatus.monsterType == MonsterType.Normar)
                    {
                        EnemyStatus status = other.GetComponent<EnemyStatus>();
                        status.current_HP = 0;
                        StartCoroutine(DeActiveGameObject());
                    }
                    else
                    {
                        SendDamageEvent sendDamageEvent = new SendDamageEvent(subject, damage);
                        actor.OnReceiveEvent(sendDamageEvent);
                        StartCoroutine(DeActiveGameObject());
                    }
                }
            }
        }
    }
    IEnumerator DeActiveGameObject()
    {
        yield return new WaitForSeconds(1.5f);
        Sequence shakeSequence = DOTween.Sequence();
        shakeSequence.Append(transform.DOShakePosition(1.5f, strength: new Vector3(0.15f, 0.15f, 0.15f), vibrato: 10, randomness: 90, snapping: false));
        shakeSequence.Play();
        yield return shakeSequence.WaitForCompletion();


        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScaleY(10, 0.3f));
        yield return new WaitForSeconds(0.5f);
        mySequence.Append(transform.DOScaleX(0,0.3f)).Join(transform.DOScaleZ(0,0.3f));
        mySequence.Play();
        yield return mySequence.WaitForCompletion();

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}