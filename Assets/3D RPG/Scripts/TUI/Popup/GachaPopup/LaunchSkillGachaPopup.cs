using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LaunchSkillGachaPopup : MonoBehaviour,IPopup
{
    public static LaunchSkillGachaPopup instance;
    public Image effectBackground;

    List<GameObject> skillSlotObjects = new List<GameObject>();
    Action<int> oneMoreTimeAction;

    public GameObject gachaImage;
    private Transform gachaImageOringTransform;
    public GameObject gachaEffect;
    public GridLayoutGroup gird;
    public GameObject skillSlotPrefab;

    SkillGachaResult SkillGachaResultTest;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gachaImageOringTransform = gachaImage.transform;
    }
    public void Initialize(SkillGachaResult skillgachaResult, Action<int> onmoreTime)
    {
        foreach(var skillSlotObject in skillSlotObjects)
        {
            skillSlotObject.SetActive(false);
        }
        oneMoreTimeAction = onmoreTime;
        StartCoroutine(SetupGachaAnimaionCoroutine(skillgachaResult));
    }

    private IEnumerator SetupGachaAnimaionCoroutine(SkillGachaResult skillGachaResult)
    {
        int count = 0;
        gachaImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        while (count < 10)
        {
            gachaImage.transform.DORotate(new Vector3(0, 0, 2), 0.1f).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(0.1f);
            gachaImage.transform.DORotate(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InOutQuad);
            count++;
            yield return new WaitForSeconds(0.2f);
            if(count == 8)
            {
                effectBackground.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                effectBackground.DOColor(new Color(1, 1, 1, 1), 1f);
            }
        }
        yield return new WaitForSeconds(0.6f);
        effectBackground.DOColor(new Color(1, 1, 1, 0), 0.8f);
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 10; i++)
        {
            SkillInfo skillInfo = skillGachaResult.skillInfos[i];
            GameObject skillSlotObject;

            skillSlotObject = skillSlotObjects.Find(skill => !skill.activeSelf);

            if (skillSlotObject == null)
            {
                skillSlotObject = Instantiate(skillSlotPrefab, gird.transform);
                skillSlotObjects.Add(skillSlotObject);
            }
            skillSlotObject.SetActive(true);

            SkillSlot skillSlot = skillSlotObject.GetComponent<SkillSlot>();
            skillSlot.SetData(skillInfo);

            Image skillImage = skillSlotObject.GetComponent<Image>();
            if (skillImage != null)
            {
                skillImage.color = new Color(skillImage.color.r, skillImage.color.g, skillImage.color.b, 0);
                skillImage.DOFade(1, 3);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void OpenPopupUI()
    {
        gameObject.SetActive(true);
    }
    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }

    public void RestastSkillGacha()
    {
        oneMoreTimeAction?.Invoke(10);
    }
}
