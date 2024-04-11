using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RangeWeaponPoint : MonoBehaviour
{
    SphereCollider weaponPointCollider;
    public GameObject projectile;

    private void Awake()
    {
        weaponPointCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        if(!weaponPointCollider.enabled)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
    }
}