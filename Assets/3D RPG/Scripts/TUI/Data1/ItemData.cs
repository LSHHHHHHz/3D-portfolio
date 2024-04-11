using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string itemName;
    public string itemDescription;
    public int itemPrice;
}
public class PortionData : ItemData
{
    public int hpRecovery;
    public int mpRecovery;
}
public class EquipData : ItemData
{
    public int addAttack;
    public int addDefence;
}