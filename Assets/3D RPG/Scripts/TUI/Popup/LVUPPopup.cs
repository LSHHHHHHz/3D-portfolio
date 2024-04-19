using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LVUPPopup : MonoBehaviour, IPopup
{
    public Text lvUPText;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(ShowLevelUpPopup());
    }
    private void OnEnable()
    {
        StartCoroutine(ShowLevelUpPopup());
    }
    public void UpdateLV(int level)
    {
        lvUPText.text = "레벨이 올랐습니다 ! \n 현재 레벨 <color=#FF0000>" + level.ToString() + "</color>";
    }
    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenPopupUI()
    {
        gameObject.SetActive(true);       
    }
    IEnumerator ShowLevelUpPopup()
    {
        rectTransform.localScale = Vector3.zero;

        Sequence popupSequence = DOTween.Sequence();
        popupSequence.Append(rectTransform.DOScale(1.2f, 0.25f));
        popupSequence.Append(rectTransform.DOScale(0.9f, 0.15f));
        popupSequence.Append(rectTransform.DOScale(1.0f, 0.1f));

        yield return new WaitForSeconds(3);
        popupSequence.Append(rectTransform.DOScale(0f, 0.25f));
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
