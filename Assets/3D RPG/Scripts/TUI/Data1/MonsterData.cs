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
}
[System.Serializable]
public class BossMonster : ItemData
{
}