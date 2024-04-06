using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/ItemInfo")] 
public class ItemInfo : ScriptableObject
{
    public InfoType itemSort;
    public string itemName;
    public Sprite iconImage;
    public string itemDescription;
    public int HPRecovery;
    public int MPRecovery;
    public int additionalAttack;
    public int additionalDefence;
    public int itemPrice;
    public int slotNumber;
}
