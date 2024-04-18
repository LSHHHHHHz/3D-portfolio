using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class DetectPlayer : MonoBehaviour
{
    public List<CharacterStatusBase> targets = new List<CharacterStatusBase>();
    public CharacterStatusBase closestTarget;
    public float checkDistance = 10;
    Vector3 originPos;
    private void Awake()
    {
        originPos = transform.position;
    }
    void Update()
    {
        closestTarget = null;
        Collider[] colliders = Physics.OverlapSphere(originPos, checkDistance);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("ForEnemyDetection"))
            {
                CharacterStatusBase target = collider.GetComponent<CharacterStatusBase>();
                if (target != null && !targets.Contains(target))
                {
                    targets.Add(target);
                }
            }
        }
        int i = 0;
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("ForEnemyDetection"))
            {
                i++;
            }
        }
        if(i ==0 && targets.Count>0)
        {
            targets.RemoveAt(0);
        }
        if (targets.Count > 0)
        {
            int number = UnityEngine.Random.Range(0, targets.Count);
            closestTarget = targets[number];
        }
        else
        {
            closestTarget = null;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originPos, checkDistance);
    }
}
