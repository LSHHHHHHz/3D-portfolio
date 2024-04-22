using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public class ProjectileSkill3 : ProjectileSkill
{
    public int damage;
    public IActor actor;
    public float detectionRadius = 20f;
    List<CharacterStatusBase> targetStatus = new List<CharacterStatusBase>();
    public CharacterStatusBase selectedStatus;

    public int speed = 0;
    private void Start()
    {
        SetPosition();
        DetectObjects();
        SelectedTarget();
        StartCoroutine(LaunchTarget());
    }
    void SetPosition()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + 1, 2, false));
        sequence.Play();
    }
    void DetectObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("ForPlayerDetection"))
            {
                CharacterStatusBase target = collider.GetComponent<CharacterStatusBase>();
                if (target != null && !targetStatus.Contains(target))
                {
                    targetStatus.Add(target);
                }
            }
        }
    }
    void SelectedTarget()
    {
        if (targetStatus.Count > 0)
        {
            int number = UnityEngine.Random.Range(0, targetStatus.Count);
            selectedStatus = targetStatus[number];
        }
    }
    IEnumerator LaunchTarget()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<CapsuleCollider>().enabled = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = true;


        Vector3 dir = (selectedStatus.transform.position - transform.position).normalized;
        transform.up = Vector3.Lerp(transform.up, dir, Time.deltaTime * 2);

        float angle = Vector3.Angle(transform.up, dir);
        while (angle > 0.3f)
        {
            transform.up = Vector3.Lerp(transform.up, dir, Time.deltaTime * 2);
            angle = Vector3.Angle(transform.up, dir);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        speed = 20;
        while (selectedStatus != null)
        {
            Vector3 dir2 = (selectedStatus.transform.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, dir2, Time.deltaTime * 10);
            transform.position += transform.up * speed * Time.deltaTime;
            yield return null;
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection"))
        {
            IActor actor = other.GetComponent<IActor>();
            if (actor != null)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.GetHitSkill3);
                SendDamageEvent damageEvent = new SendDamageEvent(this.actor, damage);
                actor.OnReceiveEvent(damageEvent);

            }
            Destroy(gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
