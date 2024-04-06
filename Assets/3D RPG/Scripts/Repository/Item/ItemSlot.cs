using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public Transform previousParent;
    public RectTransform slotRectTransform;

    public SlotParentType slotParentType;
    public ItemInfo itemInfo;
    public int slotNumber;
    public Image icon;
    public Text itemName;
    public Text itemDescription;
    public Text itemPrice;
    public Text statsInfo;
    public Text countText;
    public Sprite nullImage;
    public int itemCount;

    public Action onContentChanged { get; set; } = null;
    public Action onMerged { get; set; } = null;
    public Action onClicked { get; set; } = null;

    public Action onTouched { get; set; }

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (itemInfo == null)
        {
            icon.sprite = nullImage;
        }
    }
    public void OnDragMergeItems()
    {
        // item count 증가 

        onContentChanged?.Invoke();

        onClicked?.Invoke();
    }
    public void ClickedSlot()
    {
        onClicked?.Invoke();
    }
    public void SetData(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;         
        if (itemInfo == null)
        {
            return;
        }
        icon.sprite = itemInfo.iconImage;
        if (itemName != null)
        {
            itemName.text = itemInfo.itemName;
        }
        if (itemDescription != null)
        {
            itemDescription.text = itemInfo.itemDescription;
        }
        if (itemPrice != null)
        {
            itemPrice.text = itemInfo.itemPrice.ToString();
        }
        if (statsInfo != null)
        {
            if (itemInfo.itemSort == InfoType.HPPortion)
            {
                statsInfo.text = itemInfo.HPRecovery + "HP를 회복합니다.";
            }
            if (itemInfo.itemSort == InfoType.MPPortion)
            {
                statsInfo.text = itemInfo.HPRecovery + "MP를 회복합니다.";
            }
            if (itemInfo.itemSort == InfoType.Sword)
            {
                statsInfo.text = itemInfo.additionalAttack + "공격력을 증가시킵니다.";
            }
            if (itemInfo.itemSort == InfoType.Shield)
            {
                statsInfo.text = itemInfo.additionalDefence + "방어력을 증가시킵니다.";
            }
        }
    }
    public void SetData(ItemInstance itemInstance)
    {
        if (itemInstance == null || itemInstance.itemInfo == null)
        {
            ClearData();
            return;
        }
        SetData(itemInstance.itemInfo);
        if (countText != null)
        {
            countText.gameObject.SetActive(true);
            countText.text = itemInstance.count.ToString();
        }
    }
    public void SetData(ItemInfo newItemInfo, int newItemCount)
    {
        itemInfo = newItemInfo;
        itemCount = newItemCount;
    }
    public void ClearData()
    {
        itemInfo = null;
        icon.sprite = nullImage;
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }

    public bool IsEmpty()
    {
        return itemInfo == null;
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        onTouched?.Invoke();
    }
    //처음 드래그를 눌렀을 때 첫번째 데이터 저장
    //드래그 중이 아니면 첫번째 데이터 삭제
    public void OnDrag(PointerEventData eventData)
    {
        onTouched?.Invoke();
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)
        {
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
            if (!RectTransformUtility.RectangleContainsScreenPoint(slotRectTransform, eventData.position, null))
            {
            }
        }
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
