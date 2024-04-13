using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectPlayer : MonoBehaviour
{
    public List<CharacterStatusBase> targetStatus = new List<CharacterStatusBase>();
    public CharacterStatusBase closeTarget;

    private void Update()
    {
        closeTarget = null;

        if (targetStatus.Count != 0)
        {
            float closestDistance = 10;
            CharacterStatusBase closestTarget = null;

            for (int i = 0; i < targetStatus.Count; i++)
            {
                float dis = Vector3.Distance(transform.position, targetStatus[i].transform.position);
                if (dis < closestDistance)
                {
                    closestDistance = dis;
                    closestTarget = targetStatus[i];
                }
            }
            closeTarget = closestTarget;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStatusBase target = other.GetComponent<CharacterStatusBase>();
            if (target != null)
            {
                targetStatus.Add(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterStatusBase target = other.GetComponent<CharacterStatusBase>();
            if (target != null)
            {
                targetStatus.Remove(target);
            }
        }
    }
}
