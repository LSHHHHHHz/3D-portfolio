using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotUI : MonoBehaviour, ISlot
{
    public Image itemIcon;
    public Sprite nullImage;
    public Text itemCountText;
    public Image coolDownImage;

    public bool activeCoolDown = false;

    private SlotData currentSlotData;
    private SlotData savedCurrentSlotData;
    public IData currentDatatype;
    private InfoPopup infoPopupInstance;

    private Transform canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public Transform previousParent;
    public RectTransform slotRectTransform;

    public Action OnCoolDownComplete { get; set; } = null;
    public Action clickButton { get; set; } = null;
    public Action beingDragSlot { get; set; } = null;
    public Action<SlotData> endDragSlot { get; set; } = null;
    public Action<SlotData> OnDropSlot { get; set; } = null;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SetData(SlotData slotData)
    {
        currentSlotData = slotData;
        savedCurrentSlotData = currentSlotData;
    }
    public void SetData(SlotData slotData, IData userData)
    {
        currentSlotData = slotData;
        currentDatatype = userData;
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
        bool playedSound = false;
        //��� ������ �Ŵ����� ���� 2
        DragDropManager.instance.OnDrop(currentSlotData, transform, currentDatatype);

        //����� IData�� ����� ��
        if (currentDatatype == UserData.instance.equipmentData)
        {
            if (DragDropManager.instance.currentDragData.item.type != "Equip")
            {
                return;
            }
            AudioManager.instance.PlaySfx(AudioManager.Sfx.EquipItem);
            playedSound = true;
        }
        //����� IData�� ������ ��
        if (currentDatatype == UserData.instance.quickPortionSlotData)
        {
            if (DragDropManager.instance.currentDragData.item.type != "Portion")
            {
                return;
            }
            AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickSlot);
            playedSound = true;
        }
        //�Ŵ����� ������ �巡�� ������ ��� ���Կ� ���� 3 
        DragDropManager.instance.SetDropItem(transform);
        //���� �巡�� �����Ϳ� ��� �����Ͱ� ���� ��� ���� ���� (�ߺ�)
        //�պ�
        if (DragDropManager.instance.currentDragData.item.itemName == DragDropManager.instance.currentDropData.item.itemName)
        {
            DragDropManager.instance.currentDropData.count += DragDropManager.instance.currentDragData.count;
            //��� ���� �� ������ ������ �׼����� �Ѱܾ���!
            DragDropManager.instance.currentDragData.RemoveItem();
        }
        //���� �巡�� �����Ϳ� ��� �����Ͱ� �ٸ� ��� (�ߺ�)
        //����
        else
        {
            SlotData temp = DragDropManager.instance.currentDragData;
            DragDropManager.instance.currentDragData = DragDropManager.instance.currentDropData;
            DragDropManager.instance.currentDropData = temp;
        }
        if (!playedSound)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickSlot);
        }
        OnDropSlot?.Invoke(DragDropManager.instance.currentDropData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        previousParent = transform.parent;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        if (itemCountText == null)
        {
            // itemCountText.gameObject.SetActive(false);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        DragDropManager.instance.BeginDrag(currentSlotData, transform, currentDatatype); //�巹�� ������ �Ŵ����� ������ ���� 1
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
        //�Ŵ����� ������ ��� ������ �巡�׿� ���� 4
        endDragSlot?.Invoke(DragDropManager.instance.currentDragData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentDatatype == UserData.instance.quickPortionSlotData || currentDatatype == UserData.instance.quickSkillSlotData || currentSlotData.item == null || currentSlotData.item.itemPrice <= 0)
        {
            return;
        }
        if (infoPopupInstance != null)
        {
            infoPopupInstance.ClosePopupUI();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentDatatype == UserData.instance.quickPortionSlotData || currentDatatype == UserData.instance.quickSkillSlotData || currentSlotData.count == 0)
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