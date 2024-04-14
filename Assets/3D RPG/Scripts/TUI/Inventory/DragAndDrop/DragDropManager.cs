using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragDropManager : MonoBehaviour
{
    public static DragDropManager instance;

    public SlotData currentDragData;
    public Transform currentDragTransform;

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

    public void BeginDrag(SlotData slotData, Transform slotTransform)
    {
        currentDragData = slotData;
        currentDragTransform = slotTransform;
    }

    public void EndDrag()
    {
        currentDragData = null;
        currentDragTransform = null;
    }

    public void DropItem(Transform dropTransform)
    {
        if (currentDragData != null && dropTransform != null)
        {
            SkillSlotUI slot =  dropTransform.GetComponent<SkillSlotUI>();
            slot.SetData(currentDragData);
        }
    }
}
