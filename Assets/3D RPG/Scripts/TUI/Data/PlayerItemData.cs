using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemData : IPlayerData
{
    public ItemInstance item = null;
    public InfoType type;

    public string ItemName;
    public int HPRecovery;

    public object GetData()
    {
       return item;
    }
    public InfoType GetItemType()
    {
        return type;
    }

    //구매할때 꼭
    public void SetItemType(InfoType setType)
    {
        type = setType;
    }
    public void SetData(object newData)
    {
        item = newData as ItemInstance;
        type = item.itemInfo.itemType;
        ItemName = (newData as ItemInstance).itemInfo.itemName;
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

public class SlotData2
{
    public PlayerItemData ItemData;
    public int Count;
}

public class QuickSlotData
{
    public List<SlotData> SlotDatas = new List<SlotData>() { new SlotData(), new SlotData(), new SlotData(), new SlotData()};
}

public class PlayerIngamePortionData : PlayerItemData
{
}
public class PlayerEquipData : PlayerItemData
{
}