using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Droppable : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public ItemSlot slot;
    public SkillSlot skillslot;
    public void OnPointerEnter(PointerEventData eventData)
    {
    }
    public void OnPointerExit(PointerEventData eventData)
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        var draggableItem = eventData.pointerDrag.GetComponent<Draggable>();

        ItemSlot slot = draggableItem.slot;
        if (slot != null)
        {
            DragAndDrop(slot);
        }

    }
    void DragAndDrop(ItemSlot dragItemSlot)
    {
        if (slot.itemInfo == dragItemSlot.itemInfo && dragItemSlot.itemInfo != null)
        {
            ItemInventoryManager.instance.OnDragMergeItems(slot, dragItemSlot);
        }
        else if(slot.itemInfo != dragItemSlot.itemInfo)
        {
            ItemInventoryManager.instance.OnDragTempItemsInInventory(slot, dragItemSlot); 
        }
        //드랍될 슬롯과 드래그한 슬롯이 다를 경우

        //드랍될 슬롯이 비어있고 드래그한 슬롯이 비어있을 경우

        //드랍될 슬롯이 비어있고 드래그한 슬롯은 있을 경우

        //드랍될 슬롯은 있고 드래그한 슬롯이 없을 경우
    }
}
