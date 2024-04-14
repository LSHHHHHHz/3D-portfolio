using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image itemIcon;
    public Sprite nullIcon;

    private SlotData currentSlotData;

    public int unlockLevel;
    private Button slotButton;

    private Transform canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public Transform previousParent;
    public RectTransform slotRectTransform;
    public Action beingDragSlot { get; set; } = null;
    public Action endDragSlot { get; set; } = null;
    public Action OnDropSlot { get; set; } = null;
    public Action CheckPlayerLevel { get; set; } = null;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        slotButton = GetComponent<Button>();
    }
    public void SetData(SlotData slotData)
    {
        currentSlotData = slotData;
        UpdateUI();
        CheckUnlockSlot();
    }
    private void CheckUnlockSlot()
    {
        if (UnitManager.instance.player.playerStatus.currentLevel >= unlockLevel)
        {
            itemIcon.color = new Color(1,1,1,1);
            slotButton.enabled = true;
        }
        else
        {
            itemIcon.color = new Color(1, 1, 1, 0.3f);
            slotButton.enabled= false;
        }
    }

    private void UpdateUI()
    {
        if (currentSlotData != null && currentSlotData.item.iconPath != "")
        {
            itemIcon.sprite = Resources.Load<Sprite>(currentSlotData.item.iconPath);
        }
        else
        {
            itemIcon.sprite = nullIcon;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragDropManager.instance.DropItem(transform);
        OnDropSlot?.Invoke();
        DragAndDropManager.instance.SetDataInventorySlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragDropManager.instance.BeginDrag(currentSlotData,transform);
        previousParent = transform.parent;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        beingDragSlot?.Invoke();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragDropManager.instance.EndDrag();
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
}