using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterStatusBase : MonoBehaviour
{
    public Sprite characerImage;
    public float max_HP;
    public float current_HP;
    public int baseAttack;
    public int totalAttack;
    public float max_MP;
    public float current_MP;
    public string characterName;

    private void Awake()
    {
        totalAttack = baseAttack;
    }
    public void DamagedHP(int damage)
    {
        current_HP -= damage;
    }
    public void RecoveryHP(int recovery)
    {
        current_HP += recovery;
    }
    public void ConsumMP(int consume)
    {
        current_MP -= consume;
    }
    public void RecoveryMP(int recovery)
    {
        current_MP += recovery;
    }
    public void UpdateTotalAttack(int additionalAttack)
    {
        totalAttack = baseAttack + additionalAttack;
    }
}


