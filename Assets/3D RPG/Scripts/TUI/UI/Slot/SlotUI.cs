using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, ISlot
{
    public Image itemIcon;
    public Sprite nullImage;
    public Text itemCountText;

    private SlotData currentSlotData;
    private IData currentData;
    private InfoPopup infoPopupInstance;
    
    private Transform canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public Transform previousParent;
    public RectTransform slotRectTransform;
    public Action clickButton { get; set; } = null;
    public Action beingDragSlot { get; set; } = null;
    public Action endDragSlot { get; set; } = null;
    public Action OnDropSlot { get; set; } = null;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SetData(SlotData slotData)
    {
        throw new NotImplementedException();
    }
    public void SetData(SlotData slotData, IData userData)
    {
        currentSlotData = slotData;
        currentData = userData;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentSlotData != null && currentSlotData.item != null && currentSlotData.count > 0)
        {
            itemIcon.sprite = Resources.Load<Sprite>(currentSlotData.item.iconPath);
            if (itemCountText != null)
            {
                itemCountText.text = currentSlotData.count.ToString();
                itemCountText.enabled = true;
            }
        }
        else
        {
            if (nullImage != null)
            {
                itemIcon.sprite = nullImage;
            }
            if (itemCountText != null)
            {
                itemCountText.enabled = false;
            }
        }
        if (currentSlotData.count == 0)
        {
            if (itemCountText != null)
            {
                itemCountText.enabled = false;
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        OnDropSlot?.Invoke();
        DragAndDropManager.instance.SetDataInventorySlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        beingDragSlot?.Invoke();
        if (itemCountText != null)
        {
            itemCountText.gameObject.SetActive(false);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
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
                Debug.Log("버리는 팝업");
                //선택하면 버리고 선택하지 않으면 return;

            }
        }
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        endDragSlot?.Invoke();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentData == UserData.instance.quickPortionSlotData || currentData == UserData.instance.quickSkillSlotData || currentSlotData.item.itemPrice <=0)
        {
            return;
        }
        infoPopupInstance.ClosePopupUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentData == UserData.instance.quickPortionSlotData || currentData == UserData.instance.quickSkillSlotData || currentSlotData.item.itemPrice <= 0)
        {
            return;
        }
        if (infoPopupInstance == null)
        {
            infoPopupInstance = Instantiate(PopupFactory.instance.InfoPopupPrefab, PopupFactory.instance.infoPopupTransform).GetComponent<InfoPopup>();
            infoPopupInstance.transform.position = this.transform.position - new Vector3(200, 0, 0);
            infoPopupInstance.SetText(currentSlotData.item);
        }
        else
        {
            infoPopupInstance.SetText(currentSlotData.item);
            infoPopupInstance.transform.position = this.transform.position - new Vector3(200, 0, 0);
            infoPopupInstance.OpenPopupUI();
        }
    }

   
}