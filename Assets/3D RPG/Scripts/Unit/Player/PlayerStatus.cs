using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerStatus : CharacterStatusBase
{
    public int currentExp;
    public int maxExp = 100;
    public int currentLevel = 1;
    public StatusPopup playerStatusPopup;
    LVUPPopup lvUPPopup;
    private void Start()
    {
        playerStatusPopup.SetData(characterImage, characterName);
        UpdateMaxExp();
    }

    private void Update()
    {
        playerStatusPopup.SetData(characterName, max_HP, current_HP, max_MP, current_MP, maxExp,currentExp, currentLevel);

        if (currentExp >= maxExp )
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        currentExp -= maxExp;
        currentLevel++;
        UpdateMaxExp();
        if(lvUPPopup == null)
        {
            lvUPPopup = Instantiate(PopupFactory.instance.lvUpPopupPrefab,PopupFactory.instance.popupTransForm).GetComponent<LVUPPopup>();
        }
        else
        {
            lvUPPopup.ClosePopupUI();
            lvUPPopup.OpenPopupUI();
        }
        lvUPPopup.UpdateLV(currentLevel);

    }
    private void UpdateMaxExp()
    {
        maxExp = 100 * currentLevel;
    }
    public bool CheckPlayerLowHP()
    {
        return (current_HP / (float)max_HP) * 100 <= 60;
    }
}
