using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface ISlot: IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler, IPointerEnterHandler
{
    public void SetData(SlotData slotData, IData dataType);
}