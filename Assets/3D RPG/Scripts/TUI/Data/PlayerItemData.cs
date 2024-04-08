using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemData : IPlayerData
{
    public ItemInstance item = null;

    public object GetData()
    {
       return item;
    }
    public void SetData(object newData)
    {
        item = newData as ItemInstance;
    }
    public void ClearData()
    {
        item = null;
    }
    public void RemoveData(int count)
    {
        item.count -= count;
    }
}
public class PlayerItemInventoryData : PlayerItemData
{
}
public class PlayerIngamePortionData : PlayerItemData
{
}
public class PlayerEquipData : PlayerItemData
{
}