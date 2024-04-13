using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Data : ScriptableObject
{
	public List<ItemData> ItemData; // Replace 'EntityType' to an actual type that is serializable.
	public List<PortionData> PortionData; // Replace 'EntityType' to an actual type that is serializable.
	public List<EquipData> EquipData; // Replace 'EntityType' to an actual type that is serializable.
}