using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyWeaponCollider : MonoBehaviour
{
    public bool activeWeapon;
    public CapsuleCollider colliders;

    private void Update()
    {
        if (activeWeapon)
        {
            colliders.enabled = true;
        }
        else
        {
            colliders.enabled = false;
        }
    }
}
