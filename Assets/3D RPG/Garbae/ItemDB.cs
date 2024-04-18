using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/ItemDB")]
public class ItemDB : ScriptableObject
{
    public List<ItemInfo> itemDB = new List<ItemInfo>();
}
