using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopupFactory : MonoBehaviour
{
    public GameObject dialogueTriggerPopupPrefab;
    public GameObject statusPopupPrefab;
    public GameObject skillGachaPopupPrefab;
    public GameObject skillInventoryPopupPrefab;
    public GameObject portionShopPopupPrefab;
    public GameObject equipmentShopPopupPrefab;
    public GameObject selectedShopItemPopupPrefab;
    public GameObject selectedSkillAndPortionPopupPrefab;
    public static PopupFactory instance;

    public Transform popupTransForm;
    public Transform colleagueStatusPopupTransform;
    public Transform gachaTransform;
    public Transform selectTransform;
    private void Awake()
    {
        instance = this;
    }

}
