using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler, IPointerEnterHandler
{
    private Transform canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    public Transform previousParent;
    public RectTransform slotRectTransform;

    public InfoPopup infoPopupInstance;
    public InventoryType slotParentType;
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

    Button slotButton;
    public Action clickButton { get; set; } = null;
    public Action beingDragSlot { get; set; } = null;
    public Action endDragSlot { get; set; } = null;
    public Action OnDropSlot { get; set; } = null;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        slotButton = GetComponent<Button>();
    }

    private void Start()
    {
        if (itemInfo == null)
        {
            icon.sprite = nullImage;
        }
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
            if (itemInfo.itemType == InfoType.HPPortion)
            {
                statsInfo.text = itemInfo.HPRecovery + "HP�� ȸ���մϴ�.";
            }
            if (itemInfo.itemType == InfoType.MPPortion)
            {
                statsInfo.text = itemInfo.HPRecovery + "MP�� ȸ���մϴ�.";
            }
            if (itemInfo.itemType == InfoType.Sword)
            {
                statsInfo.text = itemInfo.additionalAttack + "���ݷ��� ������ŵ�ϴ�.";
            }
            if (itemInfo.itemType == InfoType.Shield)
            {
                statsInfo.text = itemInfo.additionalDefence + "������ ������ŵ�ϴ�.";
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
        if (slotParentType == InventoryType.None)
        {
            return;
        }
        OnDropSlot?.Invoke();
        DragAndDropManager.instance.SetDataInventorySlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotParentType == InventoryType.None)
        {
            return;
        }
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
        if (slotParentType == InventoryType.None)
        {
            return;
        }
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotParentType == InventoryType.None)
        {
            return;
        }
        if (transform.parent == canvas)
        {
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
            if (!RectTransformUtility.RectangleContainsScreenPoint(slotRectTransform, eventData.position, null))
            {
                Debug.Log("������ �˾�");
                //�����ϸ� ������ �������� ������ return;

            }
        }
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        endDragSlot?.Invoke();
        if (countText != null && itemInfo != null)
        {
            countText.gameObject.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (slotParentType != InventoryType.ItemInventory || itemInfo == null)
        {
            return;
        }
        infoPopupInstance.ClosePopupUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(slotParentType != InventoryType.ItemInventory || itemInfo == null)
        {
            return;
        }
        if (infoPopupInstance == null)
        {
            infoPopupInstance = Instantiate(PopupFactory.instance.InfoPopupPrefab, PopupFactory.instance.infoPopupTransform).GetComponent<InfoPopup>();
            infoPopupInstance.transform.position = this.transform.position - new Vector3(200,0,0);
           // infoPopupInstance.SetText();
        }
        else
        {
            //infoPopupInstance.SetText(itemInfo);
            infoPopupInstance.transform.position = this.transform.position - new Vector3(200, 0, 0);
            infoPopupInstance.OpenPopupUI();
        }
    }
}