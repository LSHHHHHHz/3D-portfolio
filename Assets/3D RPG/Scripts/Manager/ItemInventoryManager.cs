using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 1. 데이터와 UI 
// 데이터 관리하는 클래스 

// UI를 담당하는 클래스 (InventoryPopup) 
// - 기본적으로 UI를 담당하는 클래스에서 데이터를 관리하는 클래스로부터 데이터를 받아와서 UI 구성 


// 드래그 가능한 UI / Draggable 
// 드랍이 가능한 UI / Droppable (슬롯 칸) 
// 드래그앤드랍을 구현하는 코어 클래스 
// - 코어 클래스가 알아야 할 것 Draggable, Droppable 
// 1. 클릭을 했을 때 드래그 가능한 UI인지 확인 
// 2. 드래그 가능한 UI라면 마우스에 UI 부착 
// 3. 드랍을 할 때 해당 위치가 드랍이 가능한 UI인지 확인 
// 4. 드랍이 가능한 UI라면 드랍 가능한지 요청 (ex. 코어 클래스 -> Droppable쪽, InventorySlot : Droppable 
// 5. 드랍 승인이 되면 그쪽에 드랍 했다고 노티를 해주고 마우스에 남아있는 Draggable은 제거 
// 6. Droppable (ex. InventorySlot) 

//public class UIInventoryPopup
//{
//    public void SetData(ItemInventoryData data)
//    {
        
//    }

//    public void Refresh()
//    {
//        // Refresh InventoryDataMaanger접근 - 데이터 받아오기 
//        //InventoryDataManager.Instance;

//        // 데이터 기반으로 UI 갱신 (ex. 슬롯 채우기) 


//        InventoryDataManager.Instance.RemoveItem();
//    }
//}

//public class InventoryDataManager
//{
//    public static InventoryDataManager Instance = new InventoryDataManager();
//    public void AddItem()
//    {

//    }
//    public void RemoveItem()
//    {
        
//    }
//}


//public class InventoryData
//{
//    public List<InventoryItemData> InventoryItems = new List<InventoryItemData>();

//    public void Modify()
//    {
        
//    }

//    public void AddItem()
//    {
        
//    }

//    public InventoryItemData GetItem(int index)
//    {
//        return InventoryItems[index];
//    }

//    public void SetItem(int index, InventoryItemData item)
//    {
//        if (index >= 0 && index < InventoryItems.Count)
//        {
//            InventoryItems[index] = item;
//        }
//        else
//        {
//        }
//    }
//    public void RemoveItem()
//    {
        
//    }

//    public void Clear()
//    {
        
//    }


//    public void Save()
//    {
//        // 실제 저장된 공간에 저장하기
//    }

//    public static InventoryData Load()
//    {
//        // 실제 저장된 공간에서 로드 하기
//        return new InventoryData();
//    }
//}

//public class InventoryItemData
//{
//    public string Name;
//}

public class ItemInventoryManager : MonoBehaviour
{
    public static ItemInventoryManager instance;
    public ItemInfo selectedShopItemInfoData;
    public ItemInfo selectedInventoryItemInfoData;

    public event Action OnItemInventoryChanged;
    public event Action OnEquippedIngamePortionItemChanged;
    public event Action CharacterEquipInventoryItemBaseUpdate;

    public List<ItemInstance> myItems = new();
    public List<ItemInstance> equipPortionItems = new();
    public List<ItemInstance> equipWeaponItems = new();
    public List<ItemInstance>[] equipColleagueWeaponItems;
    public CharacterEquipInventoryItemBase characterEquipInventoryItemBase;
    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InitializeEquippedPortionSlots();
    }
    private void InitializeEquippedPortionSlots()
    {
        for (int i = 0; i < EquipSlotIndicateManager.instance.ingamePortionSlotsParent.transform.childCount; i++)
        {
            equipPortionItems.Add(new ItemInstance());
        }
        equipColleagueWeaponItems = new List<ItemInstance>[3];
        for (int i = 0; i < equipColleagueWeaponItems.Length; i++)
        {
            equipColleagueWeaponItems[i] = new List<ItemInstance>();
        }
    }
    public void AddItem(ItemInfo itemInfo)
    {
        ItemInstance existItem = myItems.Find(item => item.itemInfo == itemInfo);
        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            myItems.Add(new ItemInstance()
            {
                itemInfo = itemInfo,
                count = 1,
            });
        }
        OnItemInventoryChanged?.Invoke();
    }

    //인벤토리팝업에서 관리
    public void AddItem(ItemInfo itemInfo, int count)
    {
        bool added = false;
        for (int i = 0; i < myItems.Count; i++)
        {
            if (myItems[i] == null || myItems[i].itemInfo == null)
            {
                myItems[i] = new ItemInstance
                {
                    itemInfo = itemInfo,
                    count = count
                };
                added = true;
                break;
            }
        }

        if (!added)
        {
            myItems.Add(new ItemInstance
            {
                itemInfo = itemInfo,
                count = count
            });
        }

        OnItemInventoryChanged?.Invoke();
    }
    public void OnDragMergeItems(ItemSlot dropItemSlot, ItemSlot dragItemSlot)
    {
        ItemInstance dropItem = null;
        ItemInstance dragItem = null;
        for (int i = 0; i < myItems.Count; i++)
        {
            if (i == dropItemSlot.slotNumber)
            {
                dropItem = myItems[i];
            }
            if (i == dragItemSlot.slotNumber)
            {
                dragItem = myItems[i];
            }
        }
        dropItem.count += dragItem.count;
        myItems[dragItemSlot.slotNumber] = null;
        OnItemInventoryChanged?.Invoke();
    }
    public void OnDragTempItemsInInventory(ItemSlot dropItemSlot, ItemSlot dragItemSlot)
    {
        if (dropItemSlot.slotNumber < myItems.Count && dragItemSlot.slotNumber < myItems.Count)
        {
            ItemInstance temp = myItems[dropItemSlot.slotNumber];
            myItems[dropItemSlot.slotNumber] = myItems[dragItemSlot.slotNumber];
            myItems[dragItemSlot.slotNumber] = temp;

            OnItemInventoryChanged?.Invoke();
        }
    }
    public void ClearSlotData(ItemSlot slot, SlotParentType type)
    {
        switch (type)
        {
            case SlotParentType.InGamePortionInventory:
                for (int i = 0; i < equipPortionItems.Count; i++)
                {
                    if (i == slot.slotNumber)
                    {
                        equipPortionItems[i].itemInfo = null;
                        OnEquippedIngamePortionItemChanged?.Invoke();
                        return;
                    }
                }
                break;
            case SlotParentType.ItemInventory:
                for (int i = 0; i < myItems.Count; i++)
                {
                    if (i == slot.slotNumber)
                    {
                        myItems[i].itemInfo = null;
                        OnItemInventoryChanged?.Invoke();
                        return;
                    }
                }
                break;
        }
    }
    public void SetEquipPortion(ItemInfo info, int num, int inventoryItemSlotNum)
    {
        ItemInstance setItem = null;
        for (int i = 0; i < myItems.Count; i++)
        {
            if (i == inventoryItemSlotNum)
            {
                setItem = myItems[i];
                myItems[i] = null;
            }
        }
        if (info.itemSort == InfoType.HPPortion || info.itemSort == InfoType.MPPortion)
        {
            for (int i = 0; i < equipPortionItems.Count; i++)
            {
                if (i == num)
                {
                    equipPortionItems[i] = setItem;
                }
            }
        }
        OnItemInventoryChanged?.Invoke();
        OnEquippedIngamePortionItemChanged?.Invoke();
    }
    public void SetEquipWeapon(ItemInfo selectIteminfo, int inventoryItemSlotNum, ItemInfo preItemInfo)
    {
        ItemInstance setItem = null;
        ItemInstance preiteminfo = null;
        //직전 아이템이 있다면 그 정보 저장, 없으면 null
        if (preItemInfo != null)
        {
            preiteminfo = new ItemInstance
            {
                itemInfo = preItemInfo,
                count = 1
            };
        }
        else
        {
            preiteminfo = null;
        }
        //인벤토리 아이템에서 내가 선택한 아이템을 setItem에 저장
        //아이템 개수가 1개 이하면 선택 후 null로 바꿈
        //아이템 개수가 1개 초과면 선택 후 개수 -1
        for (int i = 0; i < myItems.Count; i++)
        {
            if (i == inventoryItemSlotNum)
            {
                setItem = new ItemInstance
                {
                    itemInfo = myItems[i].itemInfo,
                    count = 1
                };
                if (myItems[i].count <=1)
                {
                    myItems[i] = null;
                }
                else
                {
                    myItems[i] = new ItemInstance
                    {
                        itemInfo = myItems[i].itemInfo,
                        count = myItems[i].count - 1
                    };
                }
            }
        }

        for(int i =0; i< myItems.Count; i++)
        {
            if(myItems[i] == null)
            {
                myItems[i] = preiteminfo;
                break;
            }
        }
        characterEquipInventoryItemBase.EquipItem(setItem, selectIteminfo.itemSort);
        OnItemInventoryChanged?.Invoke();
    }
}
