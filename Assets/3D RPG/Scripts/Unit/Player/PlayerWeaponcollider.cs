using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWeaponcollider : MonoBehaviour
{
    public bool activeWeapon;
    public CapsuleCollider[] colliders;

    private void Update()
    {
        if (activeWeapon)
        {
            for(int i=0; i<colliders.Length; i++)
            {
                colliders[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }
    }
}
