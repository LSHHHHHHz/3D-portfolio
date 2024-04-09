using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkPannelPopup : MonoBehaviour, IPopup
{
    public Text talkText;
    public Text objName;

    void TalkText(string text)
    {
        talkText.text = text;
    }
    void ObjName(string objName)
    {
        this.objName.text = objName;
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
