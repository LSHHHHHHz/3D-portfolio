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

    public PortionData(string iconPath, string itemName, string itemDescription, string type, int hpRecovery, int mpRecovery, int coolDown, int itemPrice)
    {
        this.iconPath = iconPath;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.type = type;
        this.hpRecovery = hpRecovery;
        this.mpRecovery = mpRecovery;
        this.coolDown = coolDown;
        this.itemPrice = itemPrice;
    }
}
[System.Serializable]
public class EquipData : ItemData
{
    public int addAttack;
    public int addHp;
    public int itemNum;
    public EquipData(string iconPath, string itemName, string itemDescription, string type, int addAttack, int addHp, int itemPrice, int itemNum)
    {
        this.iconPath = iconPath;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.type = type;
        this.addAttack = addAttack;
        this.addHp = addHp;
        this.itemPrice = itemPrice;
        this.itemNum = itemNum;
    }

    public bool IsSword()
    {
        return addAttack > 0;
    }
}
[System.Serializable]
public class SkillData : ItemData
{
    public int damage;
    public int coolDown;
    public int consumMP;
    public string prefabPath;
    public SkillData(string iconPath, string itemName, string itemDescription, string type, int damage, int coolDown, int consumMP, string prefabPath)
    {
        this.iconPath = iconPath;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.type = type;
        this.damage = damage;
        this.coolDown = coolDown;
        this.consumMP = consumMP;
        this.prefabPath = prefabPath;
    }
}