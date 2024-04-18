using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string iconPath;
    public string itemName;
    public string itemDescription;
    public string type;
    public int itemPrice;
}
[System.Serializable]
public class PortionData : ItemData
{
    public int hpRecovery;
    public int mpRecovery;
    public int coolDown;
}
[System.Serializable]
public class EquipData : ItemData
{
    public int addAttack;
    public int addHp;
}
[System.Serializable]
public class SkillData : ItemData
{
    public int damage;
    public int coolDown;
    public int consumMP;
    public string prefabPath;
}