using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopupFactory : MonoBehaviour
{
    public GameObject dialogueTriggerPopupPrefab;
    public GameObject statusPopupPrefab;
    public GameObject monsterStatusPopupPrefab;
    public GameObject skillInventoryPopupPrefab;
    public GameObject ShopPopupPrefab;
    public GameObject selectedShopItemPopupPrefab;
    public GameObject selectedSkillAndPortionPopupPrefab;
    public GameObject InventoryPopupPrefab;
    public GameObject equipInventoryPopupPrefab;
    public GameObject InfoPopupPrefab;
    public GameObject healerPopupPrefab;
    public GameObject lvUpPopupPrefab;
    public static PopupFactory instance;

    public Transform popupTransForm;
    public Transform colleagueStatusPopupTransform;
    public Transform selectTransform;
    public Transform infoPopupTransform;
    public Transform monsterStatusPopupTransform;
    private void Awake()
    {
        instance = this;
    }

}
