using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColleagueStatus : CharacterStatusBase
{
    StatusPopup statusPopup;
    private void Start()
    {
        statusPopup = Instantiate(PopupFactory.instance.statusPopupPrefab, PopupFactory.instance.colleagueStatusPopupTransform).GetComponent<StatusPopup>();
    }
    private void Update()
    {
        statusPopup.SetData(max_HP, current_HP, max_MP, current_MP);
        statusPopup.SetData(characerImage, characterName);
    }
}
