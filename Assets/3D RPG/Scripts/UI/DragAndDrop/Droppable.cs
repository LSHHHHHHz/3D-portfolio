using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Droppable : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public ItemSlot slot;
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
        DragAndDrop(slot);
    }
    void DragAndDrop(ItemSlot dragItemSlot)
    {
        ItemInventoryManager.instance.MergeItems(slot, dragItemSlot);
        //����� ���԰� �巡���� ������ �ٸ� ���

        //����� ������ ����ְ� �巡���� ������ ������� ���

        //����� ������ ����ְ� �巡���� ������ ���� ���

        //����� ������ �ְ� �巡���� ������ ���� ���
    }
}
