using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : CharacterStatusBase
{
    public StatusPopup playerStatuspopup;
    private void Start()
    {
        playerStatuspopup.SetData(characerImage, characterName);
    }
    private void Update()
    {
        playerStatuspopup.SetData(max_HP,current_HP,max_MP,current_MP);
    }

}
