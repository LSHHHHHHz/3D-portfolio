using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipSlotIndicateManager : MonoBehaviour
{
    public static EquipSlotIndicateManager instance;
    public GameObject ingameSkillSlotsParent;
    public GameObject iagameSkillSlotsIndicateParent;
    public GameObject ingamePortionSlotsParent;
    public GameObject ingamePortionSlotsIndicateParent;
    public GameObject equipSlotsIndicateSword;
    public GameObject equipSlotsIndicateShield;
    private void Awake()
    {
        instance = this;
    }
    public void SetIngameSlotIndicate(InfoType type)
    {
        switch (type)
        {
            case InfoType.Skill:
                iagameSkillSlotsIndicateParent.SetActive(true);
                break;
            case InfoType.HPPortion:
            case InfoType.MPPortion:
                ingamePortionSlotsIndicateParent.SetActive(true);
                break;
            case InfoType.Sword:
                equipSlotsIndicateSword.SetActive(true);
                break;
            case InfoType.Shield:
                equipSlotsIndicateShield.SetActive(true);
                break;
        }
    }
    public void ReverseSetIngameSlotIndicate(InfoType type)
    {
        switch (type)
        {
            case InfoType.Skill:
                iagameSkillSlotsIndicateParent.SetActive(false);
                break;
            case InfoType.HPPortion:
            case InfoType.MPPortion:
                ingamePortionSlotsIndicateParent.SetActive(false);
                break;
            case InfoType.Sword:
                equipSlotsIndicateSword.SetActive(false);
                break;
            case InfoType.Shield:
                equipSlotsIndicateShield.SetActive(false);
                break;
        }
    }
}
