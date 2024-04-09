using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SkillInfo skillInfo;
    public Image icon;
    public Text countText;
    public int skillSlotNumber;
    public InventoryType inventoryType;

    private Transform canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public Transform previousParent;
    Button slotButton;
    public RectTransform slotRectTransform;

    public Action clickButton { get; set; } = null;
    public Action beingDragSlot { get; set; } = null;
    public Action OnDropSlot { get; set; } = null;
    public Sprite nullImage;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        slotButton = GetComponent<Button>();
    }

    public void SetData(SkillInfo skillinfo)
    {
        this.skillInfo= skillinfo;
        if(skillinfo ==null )
        {
            return;
        }
        icon.sprite = skillinfo.iconImage; 
    }
    public void SetData(SkillInstance skillInstance)
    {
        if(skillInstance ==null )
        {
            return;
        }
        SetData(skillInstance.skillInfo);
        if(countText !=null)
        {
            countText.gameObject.SetActive(true);
            countText.text = skillInstance.count.ToString();
        }
    }
    public void ClearData()
    {
        skillInfo = null;
        icon.sprite = nullImage;
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }
    public bool IsEmpty()
    {
        return skillInfo == null;
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
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
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
        if (countText != null && skillInfo != null)
        {
            countText.gameObject.SetActive(true);
        }
    }
}
