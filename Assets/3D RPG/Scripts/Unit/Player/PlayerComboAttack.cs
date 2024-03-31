using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerComboAttack : StateMachineBehaviour
{
    [SerializeField] string triggerName;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(triggerName);
    }
}
