using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public string monsterName;
    public string iconPath;
    public int maxHP;
    public int exp;
    public int damage;
}
[System.Serializable]
public class NormarMonster : MonsterData
{
    public NormarMonster(string monsterName, string iconPath, int maxHP, int exp, int damage)
    {
        this.monsterName = monsterName;
        this.iconPath = iconPath;
        this.maxHP = maxHP;
        this.exp = exp;
        this.damage = damage;
    }
}
[System.Serializable]
public class BossMonster : MonsterData
{
    public BossMonster(string monsterName, string iconPath, int maxHP, int exp, int damage)
    {
        this.monsterName = monsterName;
        this.iconPath = iconPath;
        this.maxHP = maxHP;
        this.exp = exp;
        this.damage = damage;
    }
}