using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    public int monsterNum;
    public Transform[] spawnPoint;
    public List<GameObject> spawnMonster;
    private List<bool> isReactivate;
    private void Start()
    {
        spawnMonster = new List<GameObject>();
        isReactivate = new List<bool>();
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject monster = PoolManager.instance.Get(monsterNum, spawnPoint[i]);
            spawnMonster.Add(monster);
            isReactivate.Add(false);
        }
    }
    private void Update()
    {
        for(int i =0; i< spawnMonster.Count; i++)
        {
            if (!spawnMonster[i].activeSelf && spawnMonster[i] != null && isReactivate[i] == false)
            {
                StartCoroutine(ReactivateMonster(spawnMonster[i], 3, i));
                isReactivate[i] = true;
            }
        }
    }
    IEnumerator ReactivateMonster(GameObject monster, float delay, int isActiveIndex)
    {
        yield return new WaitForSeconds(delay);  
        monster.SetActive(true);
        isReactivate[isActiveIndex] = false;
    }
}
