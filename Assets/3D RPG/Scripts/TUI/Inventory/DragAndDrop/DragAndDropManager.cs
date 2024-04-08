using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragAndDropManager : MonoBehaviour
{
    public static DragAndDropManager instance;

    public IPlayerData dragData;
    public InventoryType dragInventoryType;
    public IPlayerData dropData;
    public InventoryType dropInventoryType;
    public void SetDataInventorySlot()
    {
        if (dragData == dropData)
        {
            return;
        }
        if(dropData.GetData() == null)
        {
            dropData.SetData(dragData.GetData());
            dragData.ClearData();
        }
        else
        {
            ItemInstance temp = dropData.GetData() as ItemInstance;
            dropData.SetData(dragData.GetData());
            dragData.SetData(temp);
        }
    }
}
