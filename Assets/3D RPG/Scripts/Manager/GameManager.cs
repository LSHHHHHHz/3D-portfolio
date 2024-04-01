using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Canvas mainCanvas;
    public Canvas gachaCanavas;

    private void Awake()
    {
        instance = this;
    }

    public void StartSkillGacha()
    {
        mainCanvas.gameObject.SetActive(false);
        gachaCanavas.gameObject.SetActive(true);
        Instantiate(PopupFactory.instance.skillGachaPopupPrefab,PopupFactory.instance.gachaTransform);
    }
}
