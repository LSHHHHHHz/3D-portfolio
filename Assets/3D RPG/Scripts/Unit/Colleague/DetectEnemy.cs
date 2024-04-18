using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public List<EnemyStatus> enemyStatus = new List<EnemyStatus>();
    public EnemyStatus closeEnemy;

    private void Update()
    {
        closeEnemy = null;

        if (enemyStatus.Count != 0)
        {
            float closestDistance = 10;
            EnemyStatus closestEnemy = null;

            for (int i = 0; i < enemyStatus.Count; i++)
            {
                float dis = Vector3.Distance(transform.position, enemyStatus[i].transform.position);
                if (dis < closestDistance)
                {
                    closestDistance = dis;
                    closestEnemy = enemyStatus[i];
                }
            }
            closeEnemy = closestEnemy;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection"))
        {
            EnemyStatus enemy = other.GetComponent<EnemyStatus>();
            enemyStatus.Add(enemy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection"))
        {
            EnemyStatus enemy = other.GetComponent<EnemyStatus>();
            enemyStatus.Remove(enemy);
        }
    }
}
