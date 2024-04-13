using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string iconPath;
    public string itemName;
    public string itemDescription;
    public int itemPrice;
}
[System.Serializable]
public class PortionData : ItemData
{
    public int hpRecovery;
    public int mpRecovery;
}
[System.Serializable]
public class EquipData : ItemData
{
    public int addAttack;
    public int addHp;
}