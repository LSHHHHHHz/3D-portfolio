using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PasserbyNPC : MonoBehaviour
{
    public NPCWarePoint warePoints;
    public float oringSpeed;
    private DialogueTriggerPopup dialogueTriggerPopupInstatnce;
    private Coroutine lookAtCoroutine;
    private Animator anim;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        warePoints= GetComponent<NPCWarePoint>();
    }
    private void Start()
    {
        oringSpeed = warePoints.moveSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            warePoints.moveSpeed = 0;
            if (lookAtCoroutine != null)
            {
                StopCoroutine(lookAtCoroutine);
            }
            lookAtCoroutine = StartCoroutine(LookTarget(other.transform.position, 2f));
            anim.SetBool("IsWalk", false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            warePoints.moveSpeed = oringSpeed;
            if (lookAtCoroutine != null)
            {
                StopCoroutine(lookAtCoroutine);
                lookAtCoroutine = null;
            }
            anim.SetBool("IsWalk", true);
        }
    }
    private IEnumerator LookTarget(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(targetPosition - transform.position);

        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endRotation;
    }
}
