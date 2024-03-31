using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCAnimation : MonoBehaviour
{
    public Animator npcAnim;

    private void Awake()
    {
        npcAnim = GetComponent<Animator>();
    }
}
