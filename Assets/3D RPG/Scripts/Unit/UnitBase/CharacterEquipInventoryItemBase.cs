using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class CharacterEquipInventoryItemBase : MonoBehaviour
{
    public ItemSlot[] ingameItemslots;
    public ItemInstance swordInstance;
    public Image ImageSword;
    public ItemInstance shieldInstance;
    public Image ImageShield;
    private void Awake()
    {
        ingameItemslots = GetChildItemSlots(this.GetComponent<RectTransform>());
    }
    ItemSlot[] GetChildItemSlots(RectTransform parent)
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        for (int i = 0; i < parent.childCount; i++)
        {
            ItemSlot slot = parent.GetChild(i).GetComponent<ItemSlot>();
            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.AddListener((() =>
            {
                if(SelectedSlotInfoManager.instance.selectedItemInfo != null )
                {
                    ItemInventoryManager.instance.SetEquipWeapon(SelectedSlotInfoManager.instance.selectedItemInfo, SelectedSlotInfoManager.instance.selectedItemInfoNum, slot.itemInfo);
                    SelectedSlotInfoManager.instance.ClearItemInfo();
                    EquipSlotIndicateManager.instance.ReverseSetIngameSlotIndicate(slot.itemInfo.itemSort);
                }
            }));
            slots.Add(slot);
        }
        return slots.ToArray();
    }
    public virtual void EquipItem(ItemInstance item, InfoType type)
    {
        switch (type)
        {
            case InfoType.Sword:
                swordInstance = item;
                ingameItemslots[0].itemInfo = swordInstance.itemInfo;
                break;
            case InfoType.Shield:
                shieldInstance = item;
                ingameItemslots[1].itemInfo = shieldInstance.itemInfo;
                break;
        }
        UpdateWeaponImage();
    }
    public void UpdateWeaponImage()
    {
        if (swordInstance != null && swordInstance.itemInfo != null)
        {
            ImageSword.sprite = swordInstance.itemInfo.iconImage;
            ImageSword.gameObject.SetActive(true);
        }
        if (shieldInstance != null && shieldInstance.itemInfo != null)
        {
            ImageShield.sprite = shieldInstance.itemInfo.iconImage;
            ImageShield.gameObject.SetActive(true);
        }
    }
}