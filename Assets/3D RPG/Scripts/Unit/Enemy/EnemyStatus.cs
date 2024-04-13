using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyStatus : CharacterStatusBase
{
    public DetectPlayer detectPlayer;
    public NavMeshAgent navMeshAgent;
    public Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
    }
}
