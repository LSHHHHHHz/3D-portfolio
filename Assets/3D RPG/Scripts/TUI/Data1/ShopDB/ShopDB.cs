using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/ShopDB")]
public class ShopDB : ScriptableObject
{
    public List<ItemData> ItemData = new List<ItemData>();
}
