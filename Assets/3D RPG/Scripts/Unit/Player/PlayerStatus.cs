using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerStatus : CharacterStatusBase
{
    public int currentExp;
    public int maxExp;
    public int currentLevel;
    public StatusPopup playerStatuspopup;
    private void Start()
    {
        playerStatuspopup.SetData(characerImage, characterName);
    }
    private void Update()
    {
        playerStatuspopup.SetData(characterName, max_HP,current_HP,max_MP,current_MP);
    }

}
