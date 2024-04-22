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
        this.damage = damage;
        this.subject = actor;
        gameObject.SetActive(true);
        StartCoroutine(EnergySkill(actor));
    }
    IEnumerator EnergySkill(IActor actor)
    {
        UnitManager.instance.player.playerController.EnableMovement(false);
        Animator animator = UnitManager.instance.player.GetComponent<Animator>();
        animator.SetTrigger("Skill2");
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Player player = UnitManager.instance.player;

        if (UnitManager.instance.player.playerTargettingEnemy.targetObj != null)
        {
            targetPos = UnitManager.instance.player.playerTargettingEnemy.targetObj.transform.position;
            transform.position = UnitManager.instance.player.transform.position + new Vector3(0, 3.5f, 0);

            Sequence yoyoSequence = DOTween.Sequence();
            yoyoSequence.Append(transform.DOScale(0.2f, 0.8f)).Append(transform.DOScale(0.1f, 0.8f));
            yoyoSequence.SetLoops(5, LoopType.Yoyo);
            yoyoSequence.Play();
            yield return yoyoSequence.WaitForCompletion();
            yoyoSequence.Kill(); 

            yield return new WaitForSeconds(0.1f);
            Sequence sizeUpSequence = DOTween.Sequence();
            sizeUpSequence.Append(transform.DOScale(0.4f, 0.2f));
            sizeUpSequence.Play();
            yield return sizeUpSequence.WaitForCompletion();
            sizeUpSequence.Kill(); 

            float elapsed = 0;
            player.playerController.EnableMovement(true);
            while (elapsed < 5)
            {
                Vector3 dir = (targetPos - transform.position).normalized;
                transform.position += dir * objSpeed * Time.deltaTime;
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        yield return new WaitForSeconds(2f);
       // StartCoroutine(DeActiveGameObject());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection"))
        {
            IActor actor = other.GetComponent<IActor>();
            if (actor != null)
            {
                if (actor is CharacterStatusBase actorStatus)
                {
                    if (actorStatus.monsterType == MonsterType.Normar)
                    {
                        EnemyStatus status = other.GetComponent<EnemyStatus>();
                        status.current_HP = 0;
                      //  StartCoroutine(DeActiveGameObject());
                    }
                    else
                    {
                        SendDamageEvent sendDamageEvent = new SendDamageEvent(subject, damage);
                        actor.OnReceiveEvent(sendDamageEvent);
                      //  StartCoroutine(DeActiveGameObject());
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
        shakeSequence.Kill();

        Sequence lastSequence = DOTween.Sequence();
        lastSequence.Append(transform.DOScaleY(10, 0.3f));
        yield return new WaitForSeconds(0.5f);
        lastSequence.Append(transform.DOScaleX(0, 0.3f)).Join(transform.DOScaleZ(0, 0.3f));
        lastSequence.Play();
        yield return lastSequence.WaitForCompletion();
        lastSequence.Kill(); 

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}