using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPopup : MonoBehaviour, IPopup
{
    public Image characterImage;
    public Image hpBar;
    public Text hpBarText;
    public Image mpBar;
    public Text mpBarText;
    public Image expBar;
    public Text expBarText;
    public Text characterName;
    public Text level;

    public void SetData(string name, float maxHP, float currentHP, float maxMP, float currentMP, float maxExp, float currentExp, int level)
    {
        characterName.text = name;
        hpBarText.text = currentHP + "/" + maxHP;
        hpBar.fillAmount = currentHP / maxHP;
        mpBarText.text = (currentMP + "/" + maxMP);
        mpBar.fillAmount = currentMP / maxMP;
        expBarText.text =(currentExp + "/" + maxExp);
        expBar.fillAmount = currentExp / maxExp;
        this.level.text = "LV " + level;

    }
    public void SetData(string name, float maxHP, float currentHP, float maxMP, float currentMP)
    {
        characterName.text = name;
        hpBarText.text = currentHP + "/" + maxHP;
        hpBar.fillAmount = currentHP / maxHP;
        mpBarText.text =(currentMP + "/" + maxMP);
        mpBar.fillAmount = currentMP / maxMP;        
    }
    public void SetData(Sprite characterImage, string characterName)
    {
        this.characterImage.sprite = characterImage;
        this.characterName.text = characterName;
    }
    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenPopupUI()
    {
        gameObject.SetActive(true);
    }
}
