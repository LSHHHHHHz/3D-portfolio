using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectManager : MonoBehaviour
{
    public static DetectManager instance;
    public PlayerStatus playerStatus;
    public EnemyStatus playerDetectedEnemyStatus;
    public EnemyStatus meleeColleagueDetectedEnemyStatus;
    public EnemyStatus rangeColleagueDetectedEnemyStatus;
    public EnemyStatus buttonEnemyStatus;

    public bool checkEnemyDetect;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        
    }
    void PlayerDetectEnemy()
    {

    }
    public void EnemyDetectTrue()
    {
        checkEnemyDetect= true;
    }
    public void EnemyDetectFalse()
    {
        checkEnemyDetect= false;
    }
}
