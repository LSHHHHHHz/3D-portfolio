using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyCiollider : MonoBehaviour
{
    public FSMController monsterController;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            monsterController.OnGetHit(); 
        }
    }
}
