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
        //����� ���԰� �巡���� ������ �ٸ� ���

        //����� ������ ����ְ� �巡���� ������ ������� ���

        //����� ������ ����ְ� �巡���� ������ ���� ���

        //����� ������ �ְ� �巡���� ������ ���� ���
    }
}
