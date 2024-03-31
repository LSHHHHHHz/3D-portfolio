using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    public PlayerController playerController;
    Animator anim;

    //ÄÞº¸
    int hashAttackCount = Animator.StringToHash("AttackCount"); 
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        TryGetComponent(out anim);
    }
    private void Update()
    {
        MovePlayerAnim();
        JumpPlayerAnim();
        PlayerAttackAnim();
    }

    public int AttackCount
    {
        get => anim.GetInteger(hashAttackCount);
        set => anim.SetInteger(hashAttackCount, value);
    }

    void MovePlayerAnim()
    {
        if(playerController.currentSpeed>0.5f)
        {
            anim.SetBool("IsWalk", true);
        }
        else
        {
            anim.SetBool("IsWalk", false);
        }
        transform.LookAt(transform.position + playerController.playerMoveDir);
    }
    void JumpPlayerAnim()
    {
        if(playerController.isGrounded == false)
        {
            anim.SetTrigger("DoJump");
        }
        else
        {
            anim.SetBool("IsJump", false);
        }
    }
    void PlayerAttackAnim()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("IsAttack");
        }
    }
    
}
