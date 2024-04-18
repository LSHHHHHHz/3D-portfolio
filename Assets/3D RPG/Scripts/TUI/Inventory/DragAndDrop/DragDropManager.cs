using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragDropManager : MonoBehaviour
{
    public static DragDropManager instance;

    public IData currentDragSlotDataType;
    public SlotData currentDragData;
    public Transform currentDragTransform;
    public IData currentDropSlotDataType;
    public SlotData currentDropData;
    public Transform currentDropTransform;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BeginDrag(SlotData slotData, Transform slotTransform, IData SlotDataType)
    {
        currentDragData = slotData;
        currentDragTransform = slotTransform;
        currentDragSlotDataType = SlotDataType;
    }
    public void OnDrop(SlotData slotData, Transform slotTransform, IData SlotDataType)
    {
        currentDropData = slotData;
        currentDropTransform = slotTransform;
        currentDropSlotDataType = SlotDataType;
    }

    public void EndDrag()
    {
        currentDragData = null;
        currentDragTransform = null;
    }

    public void SetDropItem(Transform dropTransform)
    {
        if (currentDragData != null && dropTransform != null)
        {
            ISlot slot =  dropTransform.GetComponent<ISlot>();
            slot.SetData(currentDragData);
        }
    }
}
