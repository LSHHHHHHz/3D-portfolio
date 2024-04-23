using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour,ISlot
{
    public Image itemIcon;
    public Sprite nullIcon;
    public IData currentDatatype;
    public SlotData currentSlotData;

    public Image coolDownImage;
    public bool activeCoolDown = false;
    public int unlockLevel;
    private Button slotButton;

    private Transform canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public Transform previousParent;
    public RectTransform slotRectTransform;
    public Action beingDragSlot { get; set; } = null;
    public Action endDragSlot { get; set; } = null;
    public Action<SlotData> OnDropSlot { get; set; } = null;
    public Action<string, string> viewSkillInfo { get; set; } = null;

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
    }
    public void SetData(SlotData slotData, IData dataType)
    {
        currentSlotData = slotData;
        currentDatatype = dataType;
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
    public void CoolDown(int coolDownTime)
    {
        if (coolDownImage != null)
        {
            coolDownImage.gameObject.SetActive(true);
            StartCoroutine(CoolDownCoroutine(coolDownTime));
        }
    }
    private IEnumerator CoolDownCoroutine(int coolDownTime)
    {
        float elapsed = 0;
        activeCoolDown = true;
        while (elapsed < coolDownTime)
        {
            elapsed += Time.deltaTime;
            coolDownImage.fillAmount = 1f - (elapsed / coolDownTime);
            yield return null;
        }
        coolDownImage.fillAmount = 0;
        coolDownImage.gameObject.SetActive(false);
        activeCoolDown = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        ISlot data = eventData.pointerDrag.GetComponent<ISlot>();
        if(data is SlotUI slotUI)
        {
            if(slotUI.currentDatatype != currentDatatype)
            {
                return;
            }
        }
        DragDropManager.instance.SetDropItem(transform);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickSlot);
        OnDropSlot?.Invoke(currentSlotData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragDropManager.instance.BeginDrag(currentSlotData,transform, currentDatatype);
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        viewSkillInfo?.Invoke(currentSlotData.item.itemName, currentSlotData.item.itemDescription);
    }
}