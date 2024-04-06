using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum SlotParentType
{
    ItemInventory,
    ItemEquipInventory,
    InGamePortionInventory,
    SkillInventory,
    IngameSkillInventory
}
public enum InfoType
{
    Skill,
    HPPortion,
    MPPortion,
    Sword,
    Shield
}
public enum CharacterType
{
    Player,
    MeleeColleague,
    RangeColleague
}
public class EnumFacotry 
{
}