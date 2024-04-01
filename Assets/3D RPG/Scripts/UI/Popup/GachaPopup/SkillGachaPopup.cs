using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillGachaPopup : MonoBehaviour,IPopup
{
    List<GameObject> skillSlotObjects = new List<GameObject>();
    Action<int> oneMoreTimeAction;

    Canvas mainCanvas;
    Canvas gachaCanvas;
    public Camera gachaCamera;
    public GameObject gachaImage;
    private Transform gachaImageOringTransform;
    public GameObject gachaEffect;
    public GridLayoutGroup gird;
    public GameObject skillSlotPrefab;

    SkillGachaResult SkillGachaResultTest;
    private void Awake()
    {
        mainCanvas = GameManager.instance.mainCanvas;
        gachaCanvas = GameManager.instance.gachaCanavas;
    }
    private void Start()
    {
        Canvas canvasComponent = gachaCanvas.GetComponent<Canvas>();
        canvasComponent.worldCamera = gachaCamera;
        gachaImageOringTransform = gachaImage.transform;
    }
    public void Initialize(SkillGachaResult skillgachaResult, Action<int> onmoreTime)
    {
        foreach(var skillSlotObject in skillSlotObjects)
        {
            skillSlotObject.SetActive(false);
        }
       this.oneMoreTimeAction = onmoreTime;
         mainCanvas.gameObject.SetActive(false);
        gachaCanvas.gameObject.SetActive(true);
        StartCoroutine(SetupGachaAnimaionCoroutine(skillgachaResult));
    }

    private IEnumerator SetupGachaAnimaionCoroutine(SkillGachaResult skillGachaResult)
    {
        gachaImage.transform.DOLocalMove(new Vector3(0, 389, 443), 0.3f).OnComplete(() =>
        {
            gachaImage.transform.DOLocalMove(new Vector3(0, 0, -650), 0.4f);
        });

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(gachaImage.transform.DOLocalRotate(new Vector3(0, 10, 0), 0.4f))
                   .Append(gachaImage.transform.DOLocalRotate(new Vector3(0, 30, 0), 0.4f))
                   .SetLoops(5, LoopType.Yoyo).OnComplete(()=>
                   {
                       gachaImage.GetComponent<Image>().DOFade(0, 1);
                   });
        yield return new WaitForSeconds(3);

        yield return new WaitForSeconds(2);
        for (int i = 0; i < skillGachaResult.skillInfos.Count; i++)
        {
            SkillInfo skillInfo = skillGachaResult.skillInfos[i];
            GameObject skillSlotObject;

            skillSlotObject = skillSlotObjects.Find(p => !p.activeSelf);

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
        throw new NotImplementedException();
    }
    public void ClosePopupUI()
    {
        mainCanvas.gameObject.SetActive(true);
        gachaCanvas.gameObject.SetActive(false);
    }

    public void RestastSkillGacha()
    {
        oneMoreTimeAction?.Invoke(10);
    }
}
