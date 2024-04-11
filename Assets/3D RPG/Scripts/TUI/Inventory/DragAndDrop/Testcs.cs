using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Assets._3D_RPG.Scripts.TUI.Inventory.DragAndDrop
{
    // 아까 작성한 데이터
    public class ItemData
    {
        
    }

    public class ItemUI : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public ItemData data;

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // 레이캐스트
            // 놓으려 하는 마우스 포인터쪽에 위치하는 SlotUI를 찾기 
            SlotUI foundSlot;

        }
    }

    public class SlotUI
    {
        
    }

    public class InventoryUI
    {
        
    }

    internal class Testcs
    {
    }
}
