using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopupFactory : MonoBehaviour
{
    public GameObject dialogueTriggerPopupPrefab;
    public GameObject statusPopupPrefab;
    public static PopupFactory instance;

    public Transform popupTransForm;
    public Transform colleagueStatusPopupTransform;
    private void Awake()
    {
        instance = this;
    }

}
