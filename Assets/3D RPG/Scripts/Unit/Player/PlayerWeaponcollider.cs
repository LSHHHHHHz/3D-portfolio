using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWeaponcollider : MonoBehaviour
{
    public bool activeWeapon;
    public CapsuleCollider[] colliders;
    public GameObject[] trail;

    private void Update()
    {
        if (activeWeapon)
        {
            for(int i=0; i<colliders.Length; i++)
            {
                colliders[i].enabled = true;
                trail[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
                trail[i].gameObject.SetActive(false);
            }
        }
    }
}
