using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragAndDropData : MonoBehaviour
{
    public static DragAndDropData instance;
    public ItemInstance draggedItem;
    public int draggedItemSlotNumber;
    public InventoryType draggedItemSlotParentType;
    public ItemInstance dropedItem;
    public int dropedItemSlotNumber;
    public InventoryType dropedItemSlotParentType;
    public event Action startRefresh;
    public static void Load()
    {
        if (instance == null)
        {
            instance = new DragAndDropData();
        }
    }
    private void Awake()
    {
        instance = this;
    }
    public void SetDragData(ItemInstance item, int dragSlotNum, InventoryType type)
    {
        draggedItem = item;
        draggedItemSlotNumber = dragSlotNum;
        draggedItemSlotParentType = type;
    }
    public void SetDropData(ItemInstance item, int dropSlotNum, InventoryType type)
    {
        dropedItem = item;
        dropedItemSlotNumber = dropSlotNum;
        dropedItemSlotParentType = type;
    }
    public void EndDrag()
    {
        draggedItem = null;
    }
    public void UpdateInventoryUI()
    {
        startRefresh?.Invoke();
    }
}
