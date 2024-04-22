using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossWarePoint : MonoBehaviour
{
    public Transform[] warePointPos;
    private int currentIndex = 0;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    public Vector3 targetPosition;
    Animator anim;
    Rigidbody rb;
    public BossEnemyStatus status;
    private void Awake()
    {
        anim= GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetBool("IsStartBoss", true);
    }
    private void Start()
    {
        UpdateTargetPosition();
        FadeController.instance.BossFadeIn();
        GameManager.instance.ChangeCameraTarget(1);
        EventManager.instance.ChangeCameraTarget(-6, 10);        
    }

    private void Update()
    {
        if (moveSpeed <= 0)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 direction = targetPosition - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (currentIndex < warePointPos.Length - 1)
            {
                currentIndex++;
                UpdateTargetPosition();
            }
            else
            {
                this.enabled = false;
                anim.SetTrigger("IsLand");
                rb.useGravity= true;
                rb.freezeRotation = true;
                StartCoroutine(StartBossIdle());
            }
        }
    }
    IEnumerator StartBossIdle()
    {
        UnitManager.instance.player.playerController.enabled = false;
        UnitManager.instance.player.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        UnitManager.instance.player.transform.position = new Vector3(-297, 12, -130);
        UnitManager.instance.player.cameraFollow.transform.position = new Vector3(-297, 2.1f, -130);
        yield return new WaitForSeconds(4.6f);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.DragonSound);
        UnitManager.instance.player.GetComponent<CharacterController>().enabled = true;
        UnitManager.instance.player.playerController.enabled = true;
        EventManager.instance.ChangeCameraTarget(2,-3);
        yield return new WaitForSeconds(2.4f);
        status.originalPosition = transform.position;
        rb.isKinematic = true;
        status.monsterFSMController.ChangeState(new IdleState());
        yield return new WaitForSeconds(1);
        FadeController.instance.BossFadeOut();
        yield return new WaitForSeconds(0.5f);
        status.navMeshAgent.enabled = true;
        GameManager.instance.ChangeCameraTarget(0);
    }
    private void UpdateTargetPosition()
    {
        targetPosition = warePointPos[currentIndex].position;
        anim.SetTrigger("SpreadWings");
    }
}
